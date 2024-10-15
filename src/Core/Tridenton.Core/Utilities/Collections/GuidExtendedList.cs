namespace Tridenton.Core.Utilities.Collections;

public abstract class GuidExtendedList<TItem> : ExtendedList<Guid, TItem>
    where TItem : RecordMarker, IGuidUnique
{
    protected GuidExtendedList(ExtendedListInvalidOperationBehavior behavior = ExtendedListInvalidOperationBehavior.Return)
        : base(behavior) { }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="items"></param>
    /// <param name="invalidOperationBehavior"></param>
    protected GuidExtendedList(IEnumerable<TItem> items, ExtendedListInvalidOperationBehavior invalidOperationBehavior = ExtendedListInvalidOperationBehavior.Return)
        : base(items, invalidOperationBehavior) { }
    
    protected sealed override Guid GenerateNewKey(TItem item)
    {
        return Guid.NewGuid();
    }
}