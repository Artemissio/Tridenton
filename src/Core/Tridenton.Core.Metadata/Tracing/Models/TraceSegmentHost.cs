namespace Tridenton.Core.Metadata.Tracing;

public sealed record TraceSegmentHost
{
    [JsonInclude]
    public string Host { get; }
    
    [JsonInclude]
    public int? Port { get; }
    
    [JsonConstructor]
    public TraceSegmentHost(string host, int? port)
    {
        Host = host;
        Port = port;
    }
}