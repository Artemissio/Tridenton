namespace Tridenton.EventLink.Internal.Application.Core.Services;

/// <summary>
/// 
/// </summary>
public interface IEventLinkSettingsProvider
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    EventLinkSettings GetSettings();
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="settings"></param>
    void SetSettings(EventLinkSettings settings);
}