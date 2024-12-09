namespace Tridenton.EventLink.Core;

public sealed record DataChangeEvent
{
    public DataChangeEventMetadata Metadata { get; init; }
    public DataChangeEventPayload Payload { get; init; }
    
    public DataChangeEvent()
    {
        Metadata = new();
        Payload = new();
    }
}