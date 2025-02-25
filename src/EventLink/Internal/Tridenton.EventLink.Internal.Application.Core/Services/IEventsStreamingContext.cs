namespace Tridenton.EventLink.Internal.Application.Core.Services;

/// <summary>
/// 
/// </summary>
public interface IEventsStreamingContext
{
    // int RemainingItemsCount { get; }
    
    /// <summary>
    /// 
    /// </summary>
    DataChangeEvent Payload { get; }
}