namespace Tridenton.Core.Utilities;

/// <summary>
/// 
/// </summary>
public interface ILaunchable
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    ValueTask<Result> StartAsync();
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    ValueTask<Result> StopAsync();
}