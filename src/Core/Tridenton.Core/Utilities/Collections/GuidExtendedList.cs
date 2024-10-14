namespace Tridenton.Core.Utilities.Collections;

public abstract class GuidExtendedList<TItem> : ExtendedList<Guid, TItem>
    where TItem : RecordMarker, IGuidUnique
{
    protected GuidExtendedList(ExtendedListInvalidOperationBehavior behavior = ExtendedListInvalidOperationBehavior.Return)
        : base(behavior) { }
    
    protected sealed override Guid GenerateNewKey(TItem item)
    {
        return Guid.NewGuid();
    }
}