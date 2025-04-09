namespace Tridenton.Core.UI.Datagrid;

public abstract class UIDatagridBase : UIComponent
{
    internal List<UIDatagridHeader> Headers { get; } = [];
    internal List<UIDatagridColumn> Columns { get; } = [];
}