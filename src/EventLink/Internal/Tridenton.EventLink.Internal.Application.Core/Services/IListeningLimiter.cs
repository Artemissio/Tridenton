namespace Tridenton.EventLink.Internal.Application.Core.Services;

/// <summary>
/// 
/// </summary>
public interface IListeningLimiter
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    ValueTask<bool> IsLimitExceededAsync();
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    ValueTask IncrementAsync();
}