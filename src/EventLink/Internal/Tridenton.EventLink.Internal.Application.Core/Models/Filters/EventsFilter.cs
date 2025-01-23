namespace Tridenton.EventLink.Internal.Application.Core.Models;

/// <summary>
/// 
/// </summary>
public abstract class EventsFilter
{
    protected readonly EventsFilteringSettings Settings;

    protected EventsFilter(IEventLinkSettingsProvider settingsProvider)
    {
        Settings = settingsProvider.GetSettings().FilteringSettings;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public abstract bool Matches(SourceEventContext context);
}