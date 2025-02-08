namespace Tridenton.Core.UI.Buttons;

public sealed class UIButtonVariant : Enumeration
{
    private UIButtonVariant(int index, string value) : base(index, value) { }

    public static readonly UIButtonVariant Default = new(1, "tridenton-ui-button-default");
    public static readonly UIButtonVariant Primary = new(2, "tridenton-ui-button-primary");
    public static readonly UIButtonVariant Icon = new(3, "tridenton-ui-button-icon");
    public static readonly UIButtonVariant Link = new(4, "tridenton-ui-button-link");
}