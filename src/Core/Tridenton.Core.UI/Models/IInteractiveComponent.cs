using Microsoft.AspNetCore.Components;

namespace Tridenton.Core.UI;

public interface IInteractiveComponent
{
    // [Parameter]
    bool Disabled { get; set; }
    
    // [Parameter]
    EventCallback OnClick { get; set; }
    
    protected virtual async ValueTask InvokeClickAsync()
    {
        if (Disabled)
        {
            return;
        }
        
        await OnClick.InvokeAsync();
    }
}