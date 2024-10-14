namespace Tridenton.Core.Utilities.Collections;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TItem"></typeparam>
public abstract class ExtendedList<TKey, TItem> : List<TItem>
    where TKey : struct, IEquatable<TKey>, IComparable<TKey>
    where TItem : RecordMarker, IUnique<TKey>
{
    /// <summary>
    /// 
    /// </summary>
    public readonly ExtendedListInvalidOperationBehavior InvalidOperationBehavior;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="invalidOperationBehavior"></param>
    protected ExtendedList(ExtendedListInvalidOperationBehavior invalidOperationBehavior = ExtendedListInvalidOperationBehavior.Return)
    {
        InvalidOperationBehavior = invalidOperationBehavior;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    public TItem this[TKey id] => this.First(i => i.Id.Equals(id));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool TryGetItem(TKey id, out TItem? item)
    {
        try
        {
            item = this[id];
            return false;
        }
        catch (InvalidOperationException)
        {
            item = null;
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    public new void Add(TItem item)
    {
        if (ItemHasEmptyId(item))
        {
            item = item with
            {
                Id = GenerateNewKey(item)
            };
        }
        
        base.Add(item);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collection"></param>
    public new void AddRange(IEnumerable<TItem> collection)
    {
        foreach (var item in collection)
        {
            Add(item);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public void Update(TItem item)
    {
        if (ItemHasEmptyId(item))
        {
            switch (InvalidOperationBehavior)
            {
                case ExtendedListInvalidOperationBehavior.ThrowException:
                    throw new InvalidOperationException("Cannot update an item with an empty Id.");

                case ExtendedListInvalidOperationBehavior.Return:
                    return;
                
                default:
                    return;
            }
        }

        if (!TryGetItem(item.Id, out var existingItem))
        {
            switch (InvalidOperationBehavior)
            {
                case ExtendedListInvalidOperationBehavior.ThrowException:
                    throw new InvalidOperationException("Cannot update an item that does not exist.");
                
                case ExtendedListInvalidOperationBehavior.Return:
                    return;
                
                default:
                    return;
            }
        }
        
        var index = IndexOf(existingItem!);

        if (index == -1)
        {
            return;
        }

        this[index] = item;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collection"></param>
    public void UpdateRange(IEnumerable<TItem> collection)
    {
        foreach (var item in collection)
        {
            Update(item);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    public void Upsert(TItem item)
    {
        if (ItemHasEmptyId(item))
        {
            Add(item);
            return;
        }
        
        Update(item);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collection"></param>
    public void UpsertRange(IEnumerable<TItem> collection)
    {
        foreach (var item in collection)
        {
            Upsert(item);
        }
    }

    public new void Remove(TItem item)
    {
        if (ItemHasEmptyId(item))
        {
            switch (InvalidOperationBehavior)
            {
                case ExtendedListInvalidOperationBehavior.ThrowException:
                    throw new InvalidOperationException("Cannot remove an item with an empty Id.");
                
                case ExtendedListInvalidOperationBehavior.Return:
                    return;
                
                default:
                    return;
            }
        }

        if (!TryGetItem(item.Id, out var existingItem))
        {
            switch (InvalidOperationBehavior)
            {
                case ExtendedListInvalidOperationBehavior.ThrowException:
                    throw new InvalidOperationException("Cannot remove an item that does not exist.");
                
                case ExtendedListInvalidOperationBehavior.Return:
                    return;
                
                default:
                    return;
            }
        }
        
        base.Remove(existingItem!);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected abstract TKey GenerateNewKey(TItem item);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private static bool ItemHasEmptyId(TItem item)
    {
        return EqualityComparer<TKey>.Default.Equals(item.Id, default);
    }
}

/// <summary>
/// 
/// </summary>
public enum ExtendedListInvalidOperationBehavior
{
    /// <summary>
    /// 
    /// </summary>
    Return,
    
    /// <summary>
    /// 
    /// </summary>
    ThrowException,
}