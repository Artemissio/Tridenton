namespace Tridenton.Internal.Core.Context;

/// <summary>
/// 
/// </summary>
public interface IRuntimeContextAccessor
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<Result<IRuntimeContext>> GetRuntimeContextAsync(CancellationToken cancellationToken = default);
}