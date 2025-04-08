using System.Text;
using Microsoft.AspNetCore.Components;

namespace Tridenton.Core.UI.Layout;

public abstract class UIFlexComponent : UIWrapperComponent
{
    [Parameter]
    public UIFlexVariant FlexVariant { get; set; }

    protected override string GetUIComponentClass()
    {
        var strBuilder = new StringBuilder();

        if (FlexVariant.HasFlag(UIFlexVariant.AlignItemsCenter))
        {
            strBuilder.Append("align-items-center ");
        }
        
        if (FlexVariant.HasFlag(UIFlexVariant.AlignItemsStart))
        {
            strBuilder.Append("align-items-start ");
        }
        
        if (FlexVariant.HasFlag(UIFlexVariant.AlignItemsEnd))
        {
            strBuilder.Append("align-items-end ");
        }
        
        if (FlexVariant.HasFlag(UIFlexVariant.AlignItemsStretch))
        {
            strBuilder.Append("align-items-stretch ");
        }
        
        if (FlexVariant.HasFlag(UIFlexVariant.JustifyContentCenter))
        {
            strBuilder.Append("justify-content-center ");
        }
        
        if (FlexVariant.HasFlag(UIFlexVariant.JustifyContentStart))
        {
            strBuilder.Append("justify-content-start ");
        }
        
        if (FlexVariant.HasFlag(UIFlexVariant.JustifyContentSpaceBetween))
        {
            strBuilder.Append("justify-content-space-between ");
        }
        
        if (FlexVariant.HasFlag(UIFlexVariant.JustifyContentSpaceAround))
        {
            strBuilder.Append("justify-content-space-around ");
        }
        
        if (FlexVariant.HasFlag(UIFlexVariant.JustifyContentSpaceEvenly))
        {
            strBuilder.Append("justify-content-space-evenly ");
        }

        if (strBuilder.Length > 0)
        {
            strBuilder.Length--;
        }
        
        return strBuilder.ToString();
    }
}