using DestinationSettings = Tridenton.EventLink.SDK.Destinations.DestinationSettings;
using SourceSettings = Tridenton.EventLink.SDK.Sources.SourceSettings;

namespace Tridenton.EventLink.Internal.Core.Models;

/// <summary>
/// 
/// </summary>
public sealed record EventLinkOptions
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