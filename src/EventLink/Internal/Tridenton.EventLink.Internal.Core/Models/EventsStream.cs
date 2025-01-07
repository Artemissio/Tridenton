using System.Collections.Concurrent;

namespace Tridenton.EventLink.Internal.Core.Models;

public sealed class EventsStream
{
    private readonly ConcurrentQueue<DataChangeEventPayload> _internalQueue;

    public EventsStream()
    {
        _internalQueue = new();
    }

    public ValueTask WriteAsync(DataChangeEventPayload payload)
    {
        _internalQueue.Enqueue(payload);
        
        return ValueTask.CompletedTask;
    }

    public ValueTask<EventsStreamingContext> ReadAsync()
    {
        var context = EventsStreamingContext.Empty;
        
        if (_internalQueue.TryDequeue(out var payload))
        {
            context = new EventsStreamingContext(_internalQueue.Count, payload);
        }
        
        return ValueTask.FromResult(context);
    }
}

/// <summary>
/// 
/// </summary>
public sealed record EventsStreamingContext
{
    /// <summary>
    /// 
    /// </summary>
    public readonly int RemainingItemsCount;
    
    /// <summary>
    /// 
    /// </summary>
    public readonly DataChangeEventPayload Payload;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="remainingItemsCount"></param>
    /// <param name="payload"></param>
    public EventsStreamingContext(int remainingItemsCount, DataChangeEventPayload payload)
    {
        ArgumentNullException.ThrowIfNull(payload);
        
        RemainingItemsCount = remainingItemsCount;
        Payload = payload;
    }

    /// <summary>
    /// 
    /// </summary>
    public static EventsStreamingContext Empty => new(0, DataChangeEventPayload.Empty);
}