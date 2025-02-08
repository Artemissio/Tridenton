using Microsoft.AspNetCore.Components;

namespace Tridenton.Core.UI;

/// <summary>
/// 
/// </summary>
public interface IInteractiveComponent
{
    bool Disabled { get; set; }
    
    EventCallback OnClick { get; set; }
}