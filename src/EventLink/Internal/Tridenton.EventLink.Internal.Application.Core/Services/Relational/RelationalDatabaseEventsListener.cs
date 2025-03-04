namespace Tridenton.EventLink.Internal.Application.Core.Services;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TSettings"></typeparam>
public abstract class RelationalDatabaseEventsListener<TSettings> : EventsListener<TSettings>
    where TSettings : RelationalDatabaseSettings
{
    protected RelationalDatabaseEventsListener(
        IEventLinkSettingsProvider settingsProvider,
        IListeningLimiter limiter,
        IEventsStream eventsStream,
        IEventsErrorsRepository errorsRepository,
        IEventTypeDeterminator eventTypeDeterminator,
        ISourceCommandParser commandParser,
        IEnumerable<EventsFilter> filters)
        : base(settingsProvider, limiter, eventsStream, errorsRepository, eventTypeDeterminator, commandParser, filters)
    {
    }
    
    protected abstract string FormatConnectionString();
}