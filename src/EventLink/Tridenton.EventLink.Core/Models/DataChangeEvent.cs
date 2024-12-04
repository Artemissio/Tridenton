namespace Tridenton.EventLink.Core;

public record DataChangeEvent
{
    public EventType Type { get; init; }
    
    public DataChangeEventTimestamps Timestamps { get; init; }

    public DataChangeEvent()
    {
        Type = EventType.None;
    }
}