namespace Tridenton.Core.Metadata.Tracing;

public sealed record TraceSegmentRequest
{
    [JsonInclude]
    public HttpMethod Method { get; }
    
    [JsonInclude]
    public string ClientAddress { get; }
    
    [JsonInclude]
    public string Path { get; }
    
    [JsonInclude]
    public string UserAgent { get; }
    
    [JsonConstructor]
    public TraceSegmentRequest(HttpMethod method, string clientAddress, string path, string userAgent)
    {
        Method = method ?? throw new ArgumentNullException(nameof(method));
        ClientAddress = clientAddress ?? throw new ArgumentNullException(nameof(clientAddress));
        Path = path ?? throw new ArgumentNullException(nameof(path));
        UserAgent = userAgent ?? throw new ArgumentNullException(nameof(userAgent));
    }
}