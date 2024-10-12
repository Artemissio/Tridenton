namespace Tridenton.Core.Utilities;

/// <summary>
/// 
/// </summary>
public interface IExecutable
{
    ValueTask<Result> ExecuteAsync(CancellationToken cancellationToken = default);
}

/// <summary>
/// 
/// </summary>
/// <typeparam name="TRequestParams"></typeparam>
public interface IExecutable<in TRequestParams>
    where TRequestParams : class
{
    ValueTask<Result> ExecuteAsync(TRequestParams @params, CancellationToken cancellationToken = default);
}

/// <summary>
/// 
/// </summary>
/// <typeparam name="TRequestParams"></typeparam>
/// <typeparam name="TResponseParams"></typeparam>
public interface IExecutable<in TRequestParams, TResponseParams>
    where TRequestParams : class
    where TResponseParams : class
{
    ValueTask<Result<TResponseParams>> ExecuteAsync(TRequestParams @params, CancellationToken cancellationToken = default);
}
