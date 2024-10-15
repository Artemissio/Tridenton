namespace Tridenton.Core.Metadata.Tracing;

public sealed class TraceSegmentsCollection : UlidExtendedList<TraceSegment>
{
    public TraceSegmentsCollection(ExtendedListInvalidOperationBehavior behavior = ExtendedListInvalidOperationBehavior.Return)
        : base(behavior) { }
}