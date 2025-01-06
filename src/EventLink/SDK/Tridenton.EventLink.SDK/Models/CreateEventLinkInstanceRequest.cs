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
    [Required(ErrorMessage = "Source Type is required")]
    public required SourceType SourceType { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    [Required(ErrorMessage = "Destination Type is required")]
    public required DestinationType DestinationType { get; init; }

    public PostgreSQLSettings? PostgreSQLSettings { get; init; }
    public RabbitMQSettings? RabbitMQSettings { get; init; }
    public WebhooksSettings? WebhooksSettings { get; init; }

    public CreateEventLinkInstanceRequest()
    {
        SourceType = SourceType.None;
        DestinationType = DestinationType.None;
    }
}