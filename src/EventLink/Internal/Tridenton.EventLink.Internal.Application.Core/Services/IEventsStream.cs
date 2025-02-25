namespace Tridenton.EventLink.Internal.Application.Core.Services;

/// <summary>
/// 
/// </summary>
public interface IEventsStream : IDisposable
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="event"></param>
    /// <returns></returns>
    ValueTask WriteAsync(DataChangeEvent @event);
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    ValueTask<bool> WaitToReadAsync();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    ValueTask<IEventsStreamingContext> ReadAsync();
}