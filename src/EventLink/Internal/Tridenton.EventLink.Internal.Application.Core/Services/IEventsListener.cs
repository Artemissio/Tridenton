namespace Tridenton.EventLink.Internal.Application.Core.Services;

/// <summary>
/// 
/// </summary>
public interface IEventsListener : ILaunchable, IPausable
{
    /// <summary>
    /// 
    /// </summary>
    ListenerStatus Status { get; }
}