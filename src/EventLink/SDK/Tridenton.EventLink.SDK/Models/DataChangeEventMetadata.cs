namespace Tridenton.EventLink.SDK;

public sealed record DataChangeEventMetadata
{
    public EventId EventId { get; }
    public Treid EventTreid { get; init; }
    public EventType Type { get; init; }
    
    public DataChangeEventTimestamps Timestamps { get; init; }
    public DataChangeEventDestination Destination { get; init; }

    public DataChangeEventMetadata()
    {
        EventId = EventId.NewId();
        EventTreid = Treid.Empty;
        Type = EventType.None;
        Destination = new();
    }
}