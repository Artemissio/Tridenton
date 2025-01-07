using System.ComponentModel.DataAnnotations;
using Tridenton.EventLink.SDK.Destinations;
using Tridenton.EventLink.SDK.Sources;
using Tridenton.SDK.Core;

namespace Tridenton.EventLink.SDK;

/// <summary>
/// 
/// </summary>
public sealed record CreateEventLinkInstanceRequest : TridentonRequest
{
    /// <summary>
    /// 
    /// </summary>
    [Required(ErrorMessage = "Title is required")]
    public required string Title { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    [Required(ErrorMessage = "Source settings are required")]
    public required SourceSettings SourceSettings { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    [Required(ErrorMessage = "Destination settings are required")]
    public required DestinationSettings DestinationSettings { get; init; }

    public CreateEventLinkInstanceRequest()
    {
    }
}