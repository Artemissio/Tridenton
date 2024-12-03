namespace Tridenton.Core.Metadata;

public sealed record Metadata
{
    [JsonInclude]
    public RequestId RequestId { get; }
    
    [JsonInclude]
    public Trace Trace { get; }

    [JsonConstructor]
    public Metadata(RequestId requestId)
    {
        RequestId = requestId;
        Trace = new(RequestId);
    }
}