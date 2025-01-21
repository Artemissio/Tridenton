namespace Tridenton.EventLink.SDK;

public sealed record DataChangeEventMetadata
{
    public EventId EventId { get; private set; }
    public EventType Type { get; init; }
    
    public DataChangeEventTimestamps Timestamps { get; init; }
    // public DataChangeEventDestination Destination { get; init; }

    public DataChangeEventMetadata()
    {
        EventId = EventId.NewId();
        Type = EventType.None;
        // Destination = new();
    }

    public static readonly DataChangeEventMetadata Empty = new()
    {
        EventId = EventId.Empty,
        Type = EventType.None,
    };
}