using System.ComponentModel.DataAnnotations;

namespace Tridenton.EventLink.SDK.Destinations;

/// <summary>
/// Marker interface for destination settings
/// </summary>
public interface IDestinationSettingsMarker {}

public sealed record DestinationSettings
{
    /// <summary>
    /// 
    /// </summary>
    [Required(ErrorMessage = "Destination Type is required")]
    public DestinationType Type { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public RabbitMQSettings? RabbitMQ { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public WebhooksSettings? Webhooks { get; init; }

    /// <summary>
    /// Initializes a new instance of <see cref="DestinationSettings"/>
    /// </summary>
    public DestinationSettings()
    {
        Type = DestinationType.None;
    }
    
    private sealed class DestinationSettingsValidationAttribute : ConditionalRequirementAttribute<DestinationSettings>
    {
        public DestinationSettingsValidationAttribute() : base(ValidateSettings) { }

        private static bool ValidateSettings(DestinationSettings settings)
        {
            if (settings.Type == DestinationType.None)
            {
                return true;
            }

            if (settings.Type == DestinationType.Webhooks)
            {
                return settings.Webhooks is not null;
            }

            if (settings.Type == DestinationType.RabbitMq)
            {
                return settings.RabbitMQ is not null;
            }

            return false;
        }
    }
}