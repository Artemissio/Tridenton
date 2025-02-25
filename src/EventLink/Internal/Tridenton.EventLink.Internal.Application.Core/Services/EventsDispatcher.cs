// ReSharper disable VirtualMemberCallInConstructor
namespace Tridenton.EventLink.Internal.Application.Core.Services;

public abstract class EventsDispatcher<TSettings> : IEventsDispatcher
    where TSettings : IDestinationSettingsMarker
{
    protected readonly TSettings Settings;
    
    private readonly IEventsStream _stream;
    
    public DispatcherStatus Status { get; private set; }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="settingsProvider"></param>
    /// <param name="stream"></param>
    protected EventsDispatcher(
        IEventLinkSettingsProvider settingsProvider,
        IEventsStream stream)
    {
        _stream = stream;
        Settings = GetSettings(settingsProvider);
        Status = DispatcherStatus.NotStarted;
    }
    
    public async ValueTask<Result> StartAsync()
    {
        if (Status == DispatcherStatus.Started || Status == DispatcherStatus.Starting)
        {
            return Result.Success;
        }

        Status = DispatcherStatus.Starting;
        
        await StartCoreAsync();
        
        Status = DispatcherStatus.Started;

        await Task.Run(async () => await StartDispatchingAsync());
        
        return Result.Success;
    }

    public async ValueTask<Result> PauseAsync()
    {
        if (Status == DispatcherStatus.Paused || Status == DispatcherStatus.Pausing)
        {
            return Result.Success;
        }

        Status = DispatcherStatus.Pausing;
        
        await PauseCoreAsync();
        
        Status = DispatcherStatus.Paused;
        
        return Result.Success;
    }

    public async ValueTask<Result> StopAsync()
    {
        if (Status == DispatcherStatus.Stopped || Status == DispatcherStatus.Stopping)
        {
            return Result.Success;
        }

        Status = DispatcherStatus.Stopping;
        
        await StopCoreAsync();
        
        Status = DispatcherStatus.Stopped;
        
        return Result.Success;
    }

    private async ValueTask StartDispatchingAsync()
    {
        while (Status == DispatcherStatus.Started && await _stream.WaitToReadAsync())
        {
            var context = await _stream.ReadAsync();

            await DispatchEventsCoreAsync(context.Payload);
        }
    }
    
    protected abstract TSettings GetSettings(IEventLinkSettingsProvider settingsProvider);
    protected abstract ValueTask StartCoreAsync();
    protected abstract ValueTask PauseCoreAsync();
    protected abstract ValueTask StopCoreAsync();
    protected abstract ValueTask DispatchEventsCoreAsync(DataChangeEvent @event);

    protected virtual void OnDispose() { }
    
    public void Dispose()
    {
        OnDispose();
        
        GC.SuppressFinalize(this);
    }
}