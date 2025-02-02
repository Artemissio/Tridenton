namespace Tridenton.EventLink.Internal.Application.Core.Services.Internal;

/// <summary>
/// 
/// </summary>
internal sealed record EventsStreamingContext : IEventsStreamingContext
{
    /// <summary>
    /// 
    /// </summary>
    public int RemainingItemsCount { get; }
    
    /// <summary>
    /// 
    /// </summary>
    public DataChangeEvent Payload { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="remainingItemsCount"></param>
    /// <param name="payload"></param>
    public EventsStreamingContext(int remainingItemsCount, DataChangeEvent payload)
    {
        ArgumentNullException.ThrowIfNull(payload);
        
        RemainingItemsCount = remainingItemsCount;
        Payload = payload;
    }

    /// <summary>
    /// 
    /// </summary>
    public static EventsStreamingContext Empty => new(0, DataChangeEvent.Empty);
}