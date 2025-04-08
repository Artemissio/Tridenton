namespace Tridenton.Core.UI.Models;

public sealed record UISelectable<T>(T Item, int Position) where T : class
{
    public bool Selected { get; set; }
}