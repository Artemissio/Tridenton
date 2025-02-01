using Microsoft.AspNetCore.Components;

namespace Tridenton.Core.UI;

public abstract class UIComponent : ComponentBase
{
    [Parameter]
    public string Class { get; set; }
    
    [Parameter]
    public string Style { get; set; }
    
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> Attributes { get; set; }
    
    protected Ulid Id { get; private set; }
    
    protected string ClassList => $"tridenton-ui {GetUIComponentClass()}{(string.IsNullOrWhiteSpace(Class) ? string.Empty : $" {Class}")}";

    protected UIComponent()
    {
        Class = Style = string.Empty;
        Attributes = [];
    }
    
    protected override void OnInitialized()
    {
        Id = Ulid.NewUlid();
    }
    
    protected override void OnAfterRender(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }
        
        OnFirstRender();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }
        
        await OnFirstRenderAsync();
    }
    
    protected virtual void OnFirstRender() { }

    protected virtual async ValueTask OnFirstRenderAsync() => await ValueTask.CompletedTask;
    
    protected abstract string GetUIComponentClass();
    
    public async ValueTask RerenderAsync() => await InvokeAsync(StateHasChanged);
}