namespace Tridenton.EventLink.Core;

public sealed record DataChangeEventMetadata
{
    public EventId EventId { get; }
    public EventType Type { get; init; }
    
    public DataChangeEventTimestamps Timestamps { get; init; }
    public DataChangeEventDestination Destination { get; init; }

    public DataChangeEventMetadata()
    {
        EventId = EventId.NewId();
        Type = EventType.None;
        Destination = new();
    }
}