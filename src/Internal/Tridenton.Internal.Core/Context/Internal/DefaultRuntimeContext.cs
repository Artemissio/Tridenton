using Microsoft.AspNetCore.Http;
using Tridenton.Internal.Core.Models;

namespace Tridenton.Internal.Core.Context.Internal;

internal sealed record DefaultRuntimeContext : IRuntimeContext
{
    public RequestId RequestId { get; }
    
    public Error? InitializationError { get; }

    public DefaultRuntimeContext(IHttpContextAccessor httpContextAccessor)
    {
        var httpContext = httpContextAccessor.HttpContext;

        if (httpContext is null)
        {
            RequestId = RequestId.Empty;
            return;
        }
        
        var httpRequest = httpContext.Request;

        if (!httpRequest.Headers.TryGetValue(Constants.RequestIdHeader, out var requestIdStr))
        {
            InitializationError = new BadRequestError("HTTP.MissingRequestId", $"{Constants.RequestIdHeader} header is missing.");
            return;
        }

        if (!RequestId.TryParse(requestIdStr.ToString(), null, out var requestId))
        {
            
        }
    }
}