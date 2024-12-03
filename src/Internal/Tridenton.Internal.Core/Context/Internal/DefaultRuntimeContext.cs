namespace Tridenton.Internal.Core.Context.Internal;

internal sealed record DefaultRuntimeContext : IRuntimeContext
{
    public RequestId RequestId { get; }
    public ILocalizationContext Localization { get; set; }

    private DefaultRuntimeContext()
    {
        RequestId = RequestId.Empty;
        Localization = DefaultLocalizationContext.Empty;
    }

    public DefaultRuntimeContext(RequestId requestId) : this()
    {
        RequestId = requestId;
    }

    public static readonly DefaultRuntimeContext Empty = new();
}