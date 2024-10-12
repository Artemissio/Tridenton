namespace Tridenton.Core.Metadata.Tracing;

public sealed record Trace
{
    private Ulid _initialRequestId;
    
    public string Id { get; private set; }
    public TraceSegmentsCollection Segments { get; }

    public Trace(Ulid initialRequestId)
    {
        _initialRequestId = initialRequestId;
        
        Segments = [];
    }
}