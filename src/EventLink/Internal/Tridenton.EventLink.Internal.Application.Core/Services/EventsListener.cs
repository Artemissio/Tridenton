// ReSharper disable VirtualMemberCallInConstructor
using System.Collections.Immutable;

namespace Tridenton.EventLink.Internal.Application.Core.Services;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TSettings"></typeparam>
public abstract class EventsListener<TSettings> : IEventsListener
    where TSettings : ISourceSettingsMarker
{
    /*
     *
     */
    
    private readonly IEventLinkSettingsProvider _settingsProvider;
    private readonly IListeningLimiter _limiter;
    private readonly IEventsStream _eventsStream;
    private readonly IEventsErrorsRepository _errorsRepository;

    /*
     *
     */
    private readonly IEventTypeDeterminator _eventTypeDeterminator;
    private readonly ISourceCommandParser _commandParser;
    private readonly ImmutableArray<EventsFilter> _filters;

    private bool _isInitialized;
    
    protected EventLinkSettings EventLinkSettings => _settingsProvider.GetSettings();
    
    protected TSettings Settings { get; private set; }
    
    public ListenerStatus Status { get; private set; }

    protected EventsListener(
        IEventLinkSettingsProvider settingsProvider,
        IListeningLimiter limiter,
        IEventsStream eventsStream,
        IEventsErrorsRepository errorsRepository,
        IEventTypeDeterminator eventTypeDeterminator,
        ISourceCommandParser commandParser,
        IEnumerable<EventsFilter> filters)
    {
        _settingsProvider = settingsProvider;
        _limiter = limiter;
        _eventsStream = eventsStream;
        _errorsRepository = errorsRepository;
        _eventTypeDeterminator = eventTypeDeterminator;
        _commandParser = commandParser;
        _filters = [..filters];

        Status = ListenerStatus.NotStarted;
        
        Settings = default!;
    }

    public async ValueTask<Result> StartAsync()
    {
        Settings = GetSettings();
        
        Status = ListenerStatus.Starting;

        try
        {
            await StartCoreAsync();
            
            if (!_isInitialized)
            {
                await InitializeCoreAsync();
                
                _isInitialized = true;
            }

            Status = ListenerStatus.Started;

            return Result.Success;
        }
        catch (Exception exception)
        {
            Status = ListenerStatus.FailedToStart;

            return new InternalServerError("", exception.Message);
        }
    }

    public async ValueTask<Result> PauseAsync()
    {
        Status = ListenerStatus.Pausing;

        await PauseCoreAsync();

        Status = ListenerStatus.Paused;

        return Result.Success;
    }

    public async ValueTask<Result> StopAsync()
    {
        Status = ListenerStatus.Stopping;

        await StopCoreAsync();

        Status = ListenerStatus.Stopped;

        return Result.Success;
    }

    protected virtual ValueTask InitializeCoreAsync()
    {
        return ValueTask.CompletedTask;
    }
    
    protected abstract TSettings GetSettings();
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected virtual ValueTask PauseCoreAsync()
    {
        return ValueTask.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected abstract ValueTask StartCoreAsync();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected abstract ValueTask StopCoreAsync();
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected abstract ValueTask DisposeCoreAsync();

    public async ValueTask DisposeAsync()
    {
        await StopAsync();
        await DisposeCoreAsync();
        
        GC.SuppressFinalize(this);
    }

    protected async ValueTask HandleCommandAsync(SourceCommand command)
    {
        var isLimitExceeded = await _limiter.IsLimitExceededAsync();

        if (isLimitExceeded)
        {
            return;
        }
        
        var eventType = await _eventTypeDeterminator.DetermineAsync(command);

        if (eventType == EventType.None)
        {
            return;
        }

        var sourceEventContext = new SourceEventContext
        {
            EventType = eventType,
            Command = command,
            ParsedPayload = SourceCommandParsedPayload.Empty,
        };

        var isAllowedByFilters = IsAllowedByFilters(sourceEventContext);

        if (!isAllowedByFilters)
        {
            return;
        }

        var commandParsingResult = await _commandParser.ParseAsync(command);

        if (commandParsingResult.Failed)
        {
            await PersistErrorAsync(commandParsingResult.Error!);
            return;
        }

        sourceEventContext = sourceEventContext with
        {
            ParsedPayload = commandParsingResult.Value,
        };

        var dataChangeEvent = ConvertToDataChangeEvent(sourceEventContext);
        
        
        await _eventsStream.WriteAsync(dataChangeEvent);
    }

    protected async ValueTask PersistErrorAsync(Error error)
    {
        await _errorsRepository.PersistErrorAsync(error);
    }

    private bool IsAllowedByFilters(SourceEventContext context)
    {
        foreach (var filter in _filters)
        {
            if (!filter.Matches(context))
            {
                return false;
            }
        }

        return true;
    }

    private static DataChangeEvent ConvertToDataChangeEvent(SourceEventContext sourceEventContext)
    {
        var metadata = new DataChangeEventMetadata
        {
            Type = sourceEventContext.EventType,
        };
        
        metadata.Timestamps.SetEventUtc();
        metadata.Timestamps.SetHandleUtc();

        var records = sourceEventContext.ParsedPayload.Properties
            .Select(Serializer.ToJsonElement)
            .ToArray();
        
        var payload = new DataChangeEventPayload
        {
            Records = records,
        };

        var dataChangeEvent = new DataChangeEvent
        {
            Metadata = metadata,
            Payload = payload,
        };
        
        return dataChangeEvent;
    }
}