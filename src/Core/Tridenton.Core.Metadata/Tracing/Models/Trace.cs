namespace Tridenton.Core.Metadata.Tracing;

public sealed record Trace
{
    private readonly RequestId _initialRequestId;
    private readonly TraceSegmentsCollection _segments;
    
    [JsonInclude]
    public string Id { get; private set; }
    
    [JsonInclude]
    public TraceSegment[] Segments => _segments.ToArray();

    [JsonConstructor]
    public Trace(RequestId initialRequestId)
    {
        _initialRequestId = initialRequestId;
        
        Id = string.Format(TracingConstants.TraceIdSegmentlessFormat, _initialRequestId);
        
        _segments = [];
    }

    public void Append(TraceSegment segment)
    {
        _segments.Add(segment);
        
        var segmentsIds = string.Join(TracingConstants.TraceSegmentIdSeparator, _segments.Select(s => s.Id));
        
        Id = string.Format(TracingConstants.TraceIdFormat, _initialRequestId, segmentsIds);
    }
}