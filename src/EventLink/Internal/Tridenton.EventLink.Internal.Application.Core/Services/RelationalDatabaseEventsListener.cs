namespace Tridenton.EventLink.Internal.Application.Core.Services;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TSettings"></typeparam>
public abstract class RelationalDatabaseEventsListener<TSettings> : EventsListener<TSettings>
    where TSettings : RelationalDatabaseSettings
{
    protected RelationalDatabaseEventsListener(
        IListeningLimiter limiter,
        IEventsStream eventsStream,
        IEventsErrorsRepository errorsRepository,
        IEventTypeDeterminator eventTypeDeterminator,
        ISourceCommandParser commandParser,
        IEnumerable<EventsFilter> filters)
        : base(limiter, eventsStream, errorsRepository, eventTypeDeterminator, commandParser, filters)
    {
    }

    protected sealed override ValueTask StartCoreAsync()
    {
        throw new NotImplementedException();
    }

    protected sealed override ValueTask StopCoreAsync()
    {
        throw new NotImplementedException();
    }
}