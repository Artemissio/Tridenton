using System.Globalization;
using Microsoft.AspNetCore.Http;
using Tridenton.Internal.Core.Models;
using Tridenton.Internal.Core.Utilities;

namespace Tridenton.Internal.Core.Context.Internal;

internal sealed class DefaultRuntimeContextAccessor : IRuntimeContextAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DefaultRuntimeContextAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async ValueTask<Result<IRuntimeContext>> GetRuntimeContextAsync(CancellationToken cancellationToken = default)
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext is null)
        {
            return DefaultRuntimeContext.Empty;
        }
        
        var httpRequest = httpContext.Request;

        if (!httpRequest.Headers.TryGetValue(HttpRequestHeaders.RequestId, out var requestIdStr))
        {
            return new BadRequestError("HTTP.MissingRequestId", $"{HttpRequestHeaders.RequestId} header is missing.");
        }

        if (!RequestId.TryParse(requestIdStr.ToString(), CultureInfo.InvariantCulture, out var requestId))
        {
            return new BadRequestError("HTTP.InvalidRequestId", $"{HttpRequestHeaders.RequestId} header is invalid.");
        }

        var runtimeContext = new DefaultRuntimeContext(requestId);

        runtimeContext = SetLocalization(runtimeContext, httpRequest);

        return runtimeContext;
    }

    private static DefaultRuntimeContext SetLocalization(DefaultRuntimeContext runtimeContext, HttpRequest httpRequest)
    {
        if (!httpRequest.Headers.TryGetValue(HttpRequestHeaders.Locale, out var locale))
        {
            return runtimeContext;
        }
        
        try
        {
            var culture = CultureInfo.GetCultureInfo(locale.ToString());
            
            runtimeContext.Localization = new DefaultLocalizationContext(culture);
        }
        catch (CultureNotFoundException)
        {
            runtimeContext.Warning = "Current culture is not supported.";
            // Ignore - the default culture (InvariantCulture) is set in DefaultRuntimeContext contructor
        }

        return runtimeContext;
    }
}