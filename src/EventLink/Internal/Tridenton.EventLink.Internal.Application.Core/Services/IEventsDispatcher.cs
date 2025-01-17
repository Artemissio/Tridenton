namespace Tridenton.EventLink.Internal.Application.Core.Services;

/// <summary>
/// 
/// </summary>
public interface IEventsDispatcher : ILaunchable, IPausable, IDisposable
{
    /// <summary>
    /// 
    /// </summary>
    DispatcherStatus Status { get; }
    
    // /// <summary>
    // /// 
    // /// </summary>
    // /// <param name="payload"></param>
    // /// <returns></returns>
    // ValueTask<Result> DispatchEventsAsync(DataChangeEventPayload payload);
}