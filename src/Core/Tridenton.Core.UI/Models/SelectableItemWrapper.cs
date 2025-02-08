namespace Tridenton.Core.UI;

public sealed record SelectableItemWrapper<T>(T Item)
{
    public bool Selected { get; set; }
}