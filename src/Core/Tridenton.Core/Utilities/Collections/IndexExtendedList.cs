namespace Tridenton.Core.Utilities.Collections;

public abstract class IndexExtendedList<TItem> : ExtendedList<int, TItem>
    where TItem : RecordMarker, IIndexUnique
{
    protected IndexExtendedList(ExtendedListInvalidOperationBehavior behavior = ExtendedListInvalidOperationBehavior.Return)
        : base(behavior) { }
    
    protected sealed override int GenerateNewKey(TItem item)
    {
        if (Count == 0)
        {
            return 1;
        }

        return this[Count - 1].Id + 1;
    }
}