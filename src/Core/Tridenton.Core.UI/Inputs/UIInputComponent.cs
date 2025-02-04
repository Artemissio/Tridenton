using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;

namespace Tridenton.Core.UI.Inputs;

[SuppressMessage("Usage", "BL0007:Component parameters should be auto properties")]
public abstract class UIInputComponent<T> : UIWrapperComponent, IInteractiveComponent
{
    private T _value;

    [Parameter]
    public T Value
    {
        get => _value;
        set
        {
            if (value is not null && value.Equals(_value))
            {
                return;
            }

            if (Disabled)
            {
                return;
            }
            
            _value = value;

            ValueChanged.InvokeAsync(new ChangeEventArgs
            {
                Value = _value,
            });
        }
    }
    
    [Parameter]
    public EventCallback<ChangeEventArgs> ValueChanged { get; set; }

    [Parameter]
    public bool Disabled { get; set; }
    
    [Parameter]
    public EventCallback OnClick { get; set; }
    
    [Parameter]
    public string Tooltip { get; set; }
    
    [Parameter]
    public string Placeholder { get; set; }

    protected UIInputComponent()
    {
        _value = Value = default!;
        Tooltip = Placeholder = string.Empty;
    }
}