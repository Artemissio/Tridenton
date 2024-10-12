namespace Tridenton.Core.Metadata.Tracing;

public sealed record TraceSegmentHost
{
    [JsonInclude]
    public string Host { get; }
    
    [JsonConstructor]
    public TraceSegmentHost(string host)
    {
        Host = host;
    }
}