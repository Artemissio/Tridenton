namespace Tridenton.Core.UI.Buttons;

public sealed class UIButtonType : Enumeration
{
    private UIButtonType(int index, string value) : base(index, value) { }
    
    public static readonly UIButtonType Default = new(1, "tridenton-ui-button-default");
    public static readonly UIButtonType Primary = new(2, "tridenton-ui-button-primary");
    public static readonly UIButtonType Link = new(3, "tridenton-ui-button-link");
}