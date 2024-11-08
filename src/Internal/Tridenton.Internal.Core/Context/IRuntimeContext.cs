using System.Globalization;
using Tridenton.Internal.Core.Models;

namespace Tridenton.Internal.Core.Context;

public interface IRuntimeContext
{
    RequestId RequestId { get; }
    ILocalizationContext Localization { get; }
    string Warning { get; }
}