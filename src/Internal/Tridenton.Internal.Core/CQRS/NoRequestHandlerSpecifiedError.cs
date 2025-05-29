namespace Tridenton.Internal.Core.CQRS;

public sealed record NoRequestHandlerSpecifiedError()
    : InternalServerError("Core.NoRequestHandlerSpecified", "No request handler was specified for this request.");