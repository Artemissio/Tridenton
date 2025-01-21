namespace Tridenton.EventLink.SDK;

public sealed record DataChangeEvent
{
    public DataChangeEventMetadata Metadata { get; init; }
    public DataChangeEventPayload Payload { get; init; }
    
    public DataChangeEvent()
    {
        Metadata = new();
        Payload = new();
    }

    public static readonly DataChangeEvent Empty = new()
    {
        Metadata = new(),
        Payload = DataChangeEventPayload.Empty,
    };
}