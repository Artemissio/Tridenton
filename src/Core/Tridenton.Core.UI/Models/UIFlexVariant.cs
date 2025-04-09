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
    JustifyContentEnd = 64,
    JustifyContentSpaceBetween = 128,
    JustifyContentSpaceAround = 256,
    JustifyContentSpaceEvenly = 512,
}