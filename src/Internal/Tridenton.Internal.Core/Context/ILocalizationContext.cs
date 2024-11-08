using System.Globalization;

namespace Tridenton.Internal.Core.Context;

public interface ILocalizationContext
{
    public CultureInfo CurrentCulture { get; }
}