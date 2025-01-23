using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tridenton.EventLink.SDK;

public sealed record EventsFilteringSettings
{
    [Required(ErrorMessage = "Event Types are required.")]
    [Description("List of event types which EventLink will track. Set wildcard '*' to track all events.")]
    public EventType[] EventTypes { get; init; }

    [Required(ErrorMessage = "Collections are required.")]
    [Description("List of collections (tables) names which EventLink will track. Set wildcard '*' to track all collections.")]
    public string[] Collections { get; init; }
    
    public EventsFilteringSettings()
    {
        EventTypes =
        [
            EventType.All
        ];

        Collections =
        [
            Constants.Wildcard,
        ];
    }
}