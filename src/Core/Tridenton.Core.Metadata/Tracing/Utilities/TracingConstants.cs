namespace Tridenton.Core.Metadata.Tracing;

public readonly struct TracingConstants
{
    public const char TraceSegmentIdSeparator = '|';
    
    public const string TraceIdHeader = "X-Tridenton-Trace-Id";
    
    public const string TraceIdSegmentlessFormat = "Root={0};";
    public const string TraceIdFormat = "Root={0}; Segments={1};";
}