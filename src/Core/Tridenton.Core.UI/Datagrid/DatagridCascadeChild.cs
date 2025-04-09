using Microsoft.AspNetCore.Components;

namespace Tridenton.Core.UI.Datagrid;

public abstract class DatagridCascadeChild : UIWrapperComponent, IDisposable
{
    [CascadingParameter]
    public required UIDatagridBase Datagrid { get; set; }
    
    public abstract void Dispose();
}