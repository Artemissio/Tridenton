namespace Tridenton.EventLink.Internal.Application.Core.Services;

/// <summary>
/// 
/// </summary>
public interface IEventsStreamingContext
{
    /// <summary>
    /// 
    /// </summary>
    int RemainingItemsCount { get; }
    
    /// <summary>
    /// 
    /// </summary>
    DataChangeEventPayload Payload { get; }
}