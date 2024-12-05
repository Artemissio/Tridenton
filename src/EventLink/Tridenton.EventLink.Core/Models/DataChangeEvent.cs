namespace Tridenton.EventLink.Core;

public record DataChangeEvent
{
    public DataChangeEventMetadata Metadata { get; init; }
    public DataChangeEventPayload Payload { get; init; }
    
    public DataChangeEvent()
    {
        Metadata = new();
        Payload = new();
    }
}