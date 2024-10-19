using Microsoft.AspNetCore.Components;

namespace Tridenton.Core.UI;

public abstract class UIWrapperComponent : UIComponent
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}