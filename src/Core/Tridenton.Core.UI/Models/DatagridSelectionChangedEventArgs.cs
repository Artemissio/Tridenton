namespace Tridenton.Core.UI.Models;

public sealed record DatagridSelectionChangedEventArgs<T>(T[] SelectedRows) where T : class;