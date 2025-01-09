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
        IEventsStream eventsStream)
        : base(limiter, eventsStream)
    {
    }
}