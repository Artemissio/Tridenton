namespace Tridenton.Core.Metadata;

public sealed record Metadata
{
    [JsonInclude]
    public Ulid RequestId { get; }
    
    [JsonInclude]
    public Trace Trace { get; }

    [JsonConstructor]
    public Metadata(Ulid requestId)
    {
        RequestId = requestId;
        Trace = new(RequestId);
    }
}