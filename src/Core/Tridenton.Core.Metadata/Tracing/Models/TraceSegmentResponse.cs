using System.Net;
using System.Net.Mime;

namespace Tridenton.Core.Metadata.Tracing;

/// <summary>
/// 
/// </summary>
public sealed record TraceSegmentResponse
{
    /// <summary>
    /// 
    /// </summary>
    public HttpStatusCode StatusCode { get; }
    
    /// <summary>
    /// 
    /// </summary>
    public ContentType ContentType { get; }
    
    /// <summary>
    /// 
    /// </summary>
    public string Content { get; }
    
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
    /// Initializes a new instance of <see cref="TraceSegmentResponse"/>
    /// </summary>
    /// <param name="statusCode">HTTP status code</param>
    /// <param name="contentType">Content type</param>
    /// <param name="content">Content string</param>
    /// <param name="headers">Headers</param>
    /// <param name="cookies">Cookies</param>
    /// <exception cref="ArgumentNullException"></exception>
    [JsonConstructor]
    public TraceSegmentResponse(
        HttpStatusCode statusCode,
        ContentType contentType,
        string content,
        KeyValueCollection headers,
        KeyValueCollection cookies)
    {
        StatusCode = statusCode;
        ContentType = contentType ?? throw new ArgumentNullException(nameof(contentType));
        Content = content ?? throw new ArgumentNullException(nameof(content));
        Headers = headers ?? throw new ArgumentNullException(nameof(headers));
        Cookies = cookies ?? throw new ArgumentNullException(nameof(cookies));
    }
}