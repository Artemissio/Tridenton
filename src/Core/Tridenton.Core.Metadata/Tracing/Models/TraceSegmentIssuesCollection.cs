namespace Tridenton.Core.Metadata.Tracing;

public sealed class TraceSegmentIssuesCollection : IndexExtendedList<TraceSegmentIssue>
{
    public TraceSegmentIssuesCollection(ExtendedListInvalidOperationBehavior behavior = ExtendedListInvalidOperationBehavior.Return)
        : base(behavior) { }
}