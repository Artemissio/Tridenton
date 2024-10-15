namespace Tridenton.Core.Metadata.Tracing;

public sealed class TraceSubsegmentsCollection : UlidExtendedList<TraceSubsegment>
{
    public TraceSubsegmentsCollection(ExtendedListInvalidOperationBehavior behavior = ExtendedListInvalidOperationBehavior.Return)
        : base(behavior) { }
}