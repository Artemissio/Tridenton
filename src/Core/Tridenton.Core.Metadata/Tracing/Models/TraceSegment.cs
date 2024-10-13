namespace Tridenton.Core.Metadata.Tracing;

public sealed record TraceSegment : TraceSubsegment
{
    /// <summary>
    /// 
    /// </summary>
    [JsonInclude]
    public TraceSubsegmentsCollection Subsegments { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="TraceSegment"/>
    /// </summary>
    /// <param name="host">Host</param>
    /// <param name="request">HTTP request</param>
    /// <param name="response">HTTP response</param>
    /// <exception cref="ArgumentNullException"></exception>
    [JsonConstructor]
    public TraceSegment(
        TraceSegmentHost host,
        TraceSegmentRequest request,
        TraceSegmentResponse response)
        : base(host, request, response)
    {
        Subsegments = [];
    }
}