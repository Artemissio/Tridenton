namespace Tridenton.EventLink.Internal.Application.Core.Models;

/// <summary>
/// 
/// </summary>
internal sealed class CollectionFilter : EventsFilter
{
    public CollectionFilter(IEventLinkSettingsProvider settingsProvider)
        : base(settingsProvider)
    {
    }

    public override bool Matches(SourceEventContext context)
    {
        return Settings.Collections.Contains(context.Command.Collection);
    }
}