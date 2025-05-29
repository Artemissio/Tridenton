namespace Tridenton.Internal.Core.CQRS.Internal;

internal sealed class Orchestrator : IOrchestrator
{
    private readonly Dictionary<Type, object> _handlers;

    public Orchestrator(Dictionary<Type, object> handlers)
    {
        _handlers = handlers;
    }

    public async Task<Result> InvokeAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : ITridentonRequest
    {
        if (!_handlers.TryGetValue(typeof(TRequest), out var handler))
        {
            return new NoRequestHandlerSpecifiedError();
        }
        
        var requestHandler = handler as IRequestHandler<TRequest>;
        
        return await requestHandler!.HandleAsync(request, cancellationToken);
    }

    public async Task<Result<TResponse>> InvokeAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) where TRequest : ITridentonRequest<TResponse>
    {
        if (!_handlers.TryGetValue(typeof(TRequest), out var handler))
        {
            return new NoRequestHandlerSpecifiedError();
        }
        
        var requestHandler = handler as IRequestHandler<TRequest, TResponse>;
        
        return await requestHandler!.HandleAsync(request, cancellationToken);
    }
}