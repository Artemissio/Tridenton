using System.Globalization;

namespace Tridenton.Internal.Core.Context.Internal;

internal sealed record DefaultLocalizationContext : ILocalizationContext
{
    public CultureInfo CurrentCulture { get; }

    private DefaultLocalizationContext()
    {
        CurrentCulture = CultureInfo.InvariantCulture;
    }

    public DefaultLocalizationContext(CultureInfo currentCulture)
    {
        CurrentCulture = currentCulture;
    }
    
    public static readonly DefaultLocalizationContext Empty = new();
}