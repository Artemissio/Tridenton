namespace Tridenton.EventLink.Internal.Application.Core.Models;

internal sealed class MainFilter : EventsFilter
{
    public MainFilter(IEventLinkSettingsProvider settingsProvider)
        : base(settingsProvider)
    {
    }

    public override bool Matches(SourceEventContext context)
    {
        if (Settings.Pairs.Length == 1 && Settings.Pairs[0] == FilteringPair.All)
        {
            return true;
        }
        
        var pair = Settings.Pairs
            .FirstOrDefault(p => p.Collection == context.Command.Collection);

        if (pair is null)
        {
            return false;
        }

        if (pair.EventTypes.Length == 1 && pair.EventTypes[0] == EventType.All)
        {
            return true;
        }

        return pair.EventTypes.Contains(context.EventType);
    }
}