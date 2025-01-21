using System.ComponentModel.DataAnnotations;

namespace Tridenton.EventLink.SDK;

public sealed record EventsFilteringSettings
{
    [Required(ErrorMessage = "Event Types are required.")]
    public EventType[] EventTypes { get; init; }

    public EventsFilteringSettings()
    {
        EventTypes =
        [
            EventType.None
        ];
    }
}