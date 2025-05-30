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
}