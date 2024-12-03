namespace Tridenton.Internal.Core.Context;

public interface IRuntimeContext
{
    RequestId RequestId { get; }
    ILocalizationContext Localization { get; }
}