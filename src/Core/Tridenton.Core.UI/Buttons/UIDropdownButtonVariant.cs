namespace Tridenton.Core.UI.Buttons;

public sealed class UIDropdownButtonVariant : Enumeration
{
    private UIDropdownButtonVariant(int index, string value) : base(index, value) { }
    
    public static readonly UIDropdownButtonVariant Default = new(1, "tridenton-ui-button-dropdown-default");
    public static readonly UIDropdownButtonVariant Primary = new(2, "tridenton-ui-button-dropdown-primary");
}