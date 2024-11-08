namespace Tridenton.Internal.Core.Utilities;

public readonly struct HttpRequestHeaders
{
    public const string RequestId = "X-Tridenton-Request-Id";
    public const string Locale = "X-Tridenton-Locale";
}

public readonly struct HttpResponseHeaders
{
    public const string Warning = "X-Tridenton-Warning";
}