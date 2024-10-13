namespace Tridenton.Core.Metadata.Tracing;

/// <summary>
/// 
/// </summary>
public record TraceSubsegment : Durable, IUlidUnique
{
    /// <summary>
    /// 
    /// </summary>
    [JsonInclude]
    public Ulid Id { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonInclude]
    public TraceSegmentHost Host { get; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonInclude]
    public TraceSegmentRequest Request { get; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonInclude]
    public TraceSegmentResponse Response { get; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonInclude]
    public TraceSegmentIssuesCollection Issues { get; }
    
    /// <summary>
    /// Initializes a new instance of <see cref="TraceSubsegment"/>
    /// </summary>
    /// <param name="host">Host</param>
    /// <param name="request">HTTP request</param>
    /// <param name="response">HTTP response</param>
    /// <exception cref="ArgumentNullException"></exception>
    [JsonConstructor]
    public TraceSubsegment(
        TraceSegmentHost host,
        TraceSegmentRequest request,
        TraceSegmentResponse response)
    {
        Host = host ?? throw new ArgumentNullException(nameof(host));
        Request = request ?? throw new ArgumentNullException(nameof(request));
        Response = response ?? throw new ArgumentNullException(nameof(response));
        Issues = [];
    }
}