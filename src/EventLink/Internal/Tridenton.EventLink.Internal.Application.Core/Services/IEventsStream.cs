namespace Tridenton.EventLink.Internal.Application.Core.Services;

/// <summary>
/// 
/// </summary>
public interface IEventsStream : IDisposable
{
    /// <summary>
    /// 
    /// </summary>
    event AsyncEventHandler OnStreamFilledAsync;
    
    /// <summary>
    /// 
    /// </summary>
    StreamStatus Status { get; }

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
    ValueTask<IEventsStreamingContext> ReadAsync();
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    ValueTask<StreamSnapshot> GetSnapshotAsync();
}