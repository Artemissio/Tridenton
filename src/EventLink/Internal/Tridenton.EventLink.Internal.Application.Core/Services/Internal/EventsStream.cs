using System.Collections.Concurrent;

namespace Tridenton.EventLink.Internal.Application.Core.Services.Internal;

internal sealed class EventsStream : IEventsStream
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

    public async ValueTask<IEventsStreamingContext> ReadAsync()
    {
        var context = EventsStreamingContext.Empty;
        
        if (_internalQueue.TryDequeue(out var payload))
        {
            context = new EventsStreamingContext(_internalQueue.Count, payload);
        }
        
        await ValueTask.CompletedTask;

        return context;
    }
}