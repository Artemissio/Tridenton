using System.Text;
using Microsoft.AspNetCore.Components;

namespace Tridenton.Core.UI.Icons;

public abstract class UIIconComponent : UIComponent
{
    [Parameter]
    public UIIconVariant Variant { get; set; }

    protected UIIconComponent()
    {
        Variant = UIIconVariant.Default;
    }

    protected sealed override string GetUIComponentClass()
    {
        var strBuilder = new StringBuilder("tridenton-ui-icon");

        if (Variant != UIIconVariant.Default)
        {
            strBuilder.Append(' ');
            strBuilder.Append(Variant);
        }
        
        return strBuilder.ToString();
    }
}

public sealed class UIIconVariant : Enumeration
{
    private UIIconVariant(int index, string value) : base(index, value) { }
    
    public static readonly UIIconVariant Default = new(1, string.Empty);
    public static readonly UIIconVariant Inverted = new(2, "tridenton-ui-icon-inverted");
}