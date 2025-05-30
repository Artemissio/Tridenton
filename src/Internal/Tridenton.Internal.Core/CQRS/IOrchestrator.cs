namespace Tridenton.Internal.Core.CQRS;

/// <summary>
/// 
/// </summary>
public interface IOrchestrator
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TRequest"></typeparam>
    /// <returns></returns>
    Task<Result> InvokeAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : ITridentonRequest;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <returns></returns>
    Task<Result<TResponse>> InvokeAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : ITridentonRequest<TResponse>
        where TResponse : class;
}