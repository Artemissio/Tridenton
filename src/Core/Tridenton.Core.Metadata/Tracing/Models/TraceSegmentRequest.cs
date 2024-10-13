using Tridenton.Core.Utilities.Collections;

namespace Tridenton.Core.Metadata.Tracing;

/// <summary>
/// 
/// </summary>
public sealed record TraceSegmentRequest
{
    /// <summary>
    /// 
    /// </summary>
    [JsonInclude]
    public HttpMethod Method { get; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonInclude]
    public string Path { get; }

    /// <summary>
    /// 
    /// </summary>
    [JsonInclude]
    public string Protocol { get; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonInclude]
    public KeyValueCollection Headers { get; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonInclude]
    public KeyValueCollection Cookies { get; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonInclude]
    public KeyValueCollection Query { get; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonInclude]
    public KeyValueCollection RouteValues { get; }
    
    /// <summary>
    /// Initializes a new instance of <see cref="TraceSegmentRequest"/>
    /// </summary>
    /// <param name="method">HTTP method</param>
    /// <param name="path">Path</param>
    /// <param name="protocol">Protocol</param>
    /// <param name="headers">Headers</param>
    /// <param name="cookies">Cookies</param>
    /// <param name="query">Query</param>
    /// <param name="routeValues">Route values</param>
    /// <exception cref="ArgumentNullException"></exception>
    [JsonConstructor]
    public TraceSegmentRequest(
        HttpMethod method,
        string path,
        string protocol,
        KeyValueCollection headers,
        KeyValueCollection cookies,
        KeyValueCollection query,
        KeyValueCollection routeValues)
    {
        Method = method ?? throw new ArgumentNullException(nameof(method));
        Path = path ?? throw new ArgumentNullException(nameof(path));
        Protocol = protocol ?? throw new ArgumentNullException(nameof(protocol));
        Headers = headers ?? throw new ArgumentNullException(nameof(headers));
        Cookies = cookies ?? throw new ArgumentNullException(nameof(cookies));
        Query = query ?? throw new ArgumentNullException(nameof(query));
        RouteValues = routeValues ?? throw new ArgumentNullException(nameof(routeValues));
    }
}