namespace Tridenton.Core.Utilities.Collections;

public abstract class UlidExtendedList<TItem> : ExtendedList<Ulid, TItem>
    where TItem : RecordMarker, IUlidUnique
{
    protected UlidExtendedList(ExtendedListInvalidOperationBehavior behavior = ExtendedListInvalidOperationBehavior.Return)
        : base(behavior) { }
    
    protected sealed override Ulid GenerateNewKey(TItem item)
    {
        return Ulid.NewUlid();
    }
}