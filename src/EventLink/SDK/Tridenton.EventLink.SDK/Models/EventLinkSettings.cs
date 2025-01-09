using Tridenton.EventLink.SDK.Destinations;
using Tridenton.EventLink.SDK.Sources;

namespace Tridenton.EventLink.SDK;

public sealed record EventLinkSettings
{
    /// <summary>
    /// 
    /// </summary>
    public ulong Limit { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public required SourceSettings SourceSettings { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public required DestinationSettings DestinationSettings { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public bool Limitless => Limit == 0;
}