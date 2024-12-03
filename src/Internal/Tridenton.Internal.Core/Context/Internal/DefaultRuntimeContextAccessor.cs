using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
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

        RequestId requestId;
        if (httpRequest.Headers.TryGetValue(HttpRequestHeaders.RequestId, out var requestIdStr))
        {
            if (!RequestId.TryParse(requestIdStr.ToString(), CultureInfo.InvariantCulture, out requestId))
            {
                return new BadRequestError("HTTP.InvalidRequestId", $"{HttpRequestHeaders.RequestId} header is invalid.");
            }
        }
        else
        {
            requestId = RequestId.NewId();
        }

        var runtimeContext = new DefaultRuntimeContext(requestId);

        var localizationSetResult = GetLocalization(httpRequest);

        if (localizationSetResult.Failed)
        {
            return localizationSetResult.Error!;
        }
        
        runtimeContext.Localization = localizationSetResult.Value;

        return runtimeContext;
    }

    private static Result<ILocalizationContext> GetLocalization(HttpRequest httpRequest)
    {
        if (!httpRequest.Headers.TryGetValue(HeaderNames.AcceptLanguage, out var locale))
        {
            return DefaultLocalizationContext.Empty;
        }
        
        var localeString = locale.ToString();

        if (localeString == Constants.Wildcard)
        {
            return DefaultLocalizationContext.Empty;
        }
        
        try
        {
            var culture = CultureInfo.GetCultureInfo(locale.ToString());
            
            return new DefaultLocalizationContext(culture);
        }
        catch (CultureNotFoundException)
        {
            return new PreconditionError("HTTP.InvalidLanguage", $"The specified language '{locale}' is not supported.");
        }
    }
}