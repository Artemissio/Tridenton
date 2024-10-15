namespace Tridenton.Core.Utilities.Collections;

public abstract class UlidExtendedList<TItem> : ExtendedList<Ulid, TItem>
    where TItem : RecordMarker, IUlidUnique
{
    protected UlidExtendedList(ExtendedListInvalidOperationBehavior behavior = ExtendedListInvalidOperationBehavior.Return)
        : base(behavior) { }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="items"></param>
    /// <param name="invalidOperationBehavior"></param>
    protected UlidExtendedList(IEnumerable<TItem> items, ExtendedListInvalidOperationBehavior invalidOperationBehavior = ExtendedListInvalidOperationBehavior.Return)
        : base(items, invalidOperationBehavior) { }
    
    protected sealed override Ulid GenerateNewKey(TItem item)
    {
        return Ulid.NewUlid();
    }
}