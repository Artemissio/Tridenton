namespace Tridenton.Core.UI;

public record TreeModel<TItem> where TItem : TreeModel<TItem>
{
    public TItem? Parent { get; init; }
    public TreeChildrenCollection<TItem> Children { get; }

    public TreeModel()
    {
        Children = [];
    }
}

public sealed class TreeChildrenCollection<TItem> :
    List<TItem> where TItem : TreeModel<TItem>
{
    
}