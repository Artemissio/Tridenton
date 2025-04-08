namespace Tridenton.Core.UI;

[Flags]
public enum UIFlexVariant
{
    AlignItemsCenter = 1,
    AlignItemsStart = 2,
    AlignItemsEnd = 4,
    AlignItemsStretch = 8,
    JustifyContentCenter = 16,
    JustifyContentStart = 32,
    JustifyContentSpaceBetween = 64,
    JustifyContentSpaceAround = 128,
    JustifyContentSpaceEvenly = 256,
}