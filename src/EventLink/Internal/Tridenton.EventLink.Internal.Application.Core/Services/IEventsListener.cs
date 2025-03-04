namespace Tridenton.EventLink.Internal.Application.Core.Services;

/// <summary>
/// 
/// </summary>
public interface IEventsListener : ILaunchable, IPausable, IAsyncDisposable
{
    /// <summary>
    /// 
    /// </summary>
    ListenerStatus Status { get; }
}