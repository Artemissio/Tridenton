namespace Tridenton.Core.Utilities;

/// <summary>
/// 
/// </summary>
public interface IPausable
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    ValueTask<Result> PauseAsync();
}