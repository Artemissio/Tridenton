using Microsoft.Extensions.DependencyInjection;

namespace Tridenton.Internal.Core.CQRS.Internal;

internal sealed class Orchestrator : IOrchestrator
{
    private readonly OrchestratorSetup _setup;
    private readonly IServiceProvider _serviceProvider;

    public Orchestrator(OrchestratorSetup setup, IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _setup = setup;
    }

    public async Task<Result> InvokeAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : ITridentonRequest
    {
        if (!_setup.Handlers.TryGetValue(typeof(TRequest), out var handlerType))
        {
            return new NoRequestHandlerSpecifiedError();
        }
        
        var handler = GetHandler(handlerType);
        
        var requestHandler = handler as IRequestHandler<TRequest>;
        
        return await requestHandler!.HandleAsync(request, cancellationToken);
    }

    public async Task<Result<TResponse>> InvokeAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) where TRequest : ITridentonRequest<TResponse>
        where TResponse : class
    {
        if (!_setup.Handlers.TryGetValue(typeof(TRequest), out var handlerType))
        {
            return new NoRequestHandlerSpecifiedError();
        }

        var handler = GetHandler(handlerType);
        
        var requestHandler = handler as IRequestHandler<TRequest, TResponse>;
        
        return await requestHandler!.HandleAsync(request, cancellationToken);
    }

    private object GetHandler(Type handlerType)
    {
        object? handler = null;

        try
        {
            handler = _serviceProvider.GetService(handlerType);
        }
        catch (Exception)
        {
            // Ignore
        }

        if (handler is not null)
        {
            return handler;
        }
        
        using var scope = _serviceProvider.CreateScope();
            
        handler = scope.ServiceProvider.GetService(handlerType);

        return handler!;
    }
}