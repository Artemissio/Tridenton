namespace Tridenton.Core.Metadata.Tracing;

public sealed record TraceSegment
{
    public int Index { get; internal set; }
    
    public TraceSegmentHost Host { get; }
}