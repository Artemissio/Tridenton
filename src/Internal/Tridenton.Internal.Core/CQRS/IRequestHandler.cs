namespace Tridenton.Internal.Core.CQRS;

/// <summary>
/// Marker interface
/// </summary>
public interface IRequestHandler;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TRequest"></typeparam>
public interface IRequestHandler<in TRequest> : IRequestHandler
    where TRequest : ITridentonRequest
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Result> HandleAsync(TRequest request, CancellationToken cancellationToken);
}

/// <summary>
/// 
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public interface IRequestHandler<in TRequest, TResponse> : IRequestHandler
    where TRequest : ITridentonRequest<TResponse>
    where TResponse : class
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Result<TResponse>> HandleAsync(TRequest request, CancellationToken cancellationToken);
}