namespace Tridenton.Core.Utilities;

public abstract class ExtendedList<TKey, TItem> : List<TItem>
    where TKey : struct
    where TItem : class, IUnique<TKey>
{
    
}