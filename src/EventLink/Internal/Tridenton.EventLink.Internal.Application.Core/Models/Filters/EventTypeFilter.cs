namespace Tridenton.EventLink.Internal.Application.Core.Models;

/// <summary>
/// 
/// </summary>
internal sealed class EventTypeFilter : EventsFilter
{
    public EventTypeFilter(IEventLinkSettingsProvider settingsProvider)
        : base(settingsProvider)
    {
    }

    public override bool Matches(SourceEventContext context)
    {
        return Settings.EventTypes.Contains(context.EventType);
    }
}