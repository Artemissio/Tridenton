using System.Collections.Concurrent;

namespace Tridenton.EventLink.Internal.Application.Core.Services.Internal;

/// <summary>
/// Default events stream implementation
/// </summary>
internal sealed class EventsStream : IEventsStream
{
    private readonly SemaphoreSlim _readSemaphore;
    private readonly SemaphoreSlim _snapshotSemaphore;
    private readonly ConcurrentQueue<DataChangeEvent> _internalQueue;
    
    public event AsyncEventHandler? OnStreamFilledAsync;

    public StreamStatus Status { get; private set; }
    
    public EventsStream()
    {
        _readSemaphore = new(1, 1);
        _snapshotSemaphore = new(1, 1);
        _internalQueue = new();
        
        Status = StreamStatus.Empty;
    }
    
    public async ValueTask WriteAsync(DataChangeEvent @event)
    {
        if (_internalQueue.Count == int.MaxValue)
        {
            Status = StreamStatus.Full;
            return;
        }
        
        _internalQueue.Enqueue(@event);
        
        Status = StreamStatus.Ok;

        if (_internalQueue.Count == 1)
        {
            await TriggerStreamFilledEventAsync();
        }
    }

    public async ValueTask<IEventsStreamingContext> ReadAsync()
    {
        await _readSemaphore.WaitAsync();

        try
        {
            var context = EventsStreamingContext.Empty;

            if (_internalQueue.IsEmpty)
            {
                Status = StreamStatus.Empty;
                return context;
            }
        
            if (_internalQueue.TryDequeue(out var payload))
            {
                context = new EventsStreamingContext(_internalQueue.Count, payload);
            }
            
            return context;
        }
        finally
        {
            _readSemaphore.Release();
        }
    }

    public async ValueTask<StreamSnapshot> GetSnapshotAsync()
    {
        await _snapshotSemaphore.WaitAsync();
        
        var amountOfEvents = _internalQueue.Count;
        
        var snapshot = new StreamSnapshot(
            status: Status,
            capacity: int.MaxValue,
            amountOfEvents: amountOfEvents,
            remainingCapacity: int.MaxValue - amountOfEvents);
        
        _snapshotSemaphore.Release();
        
        return snapshot;
    }

    public void Dispose()
    {
        _readSemaphore.Dispose();
        _snapshotSemaphore.Dispose();
        _internalQueue.Clear();
    }

    private async ValueTask TriggerStreamFilledEventAsync()
    {
        if (OnStreamFilledAsync is null)
        {
            return;
        }
        
        await OnStreamFilledAsync.Invoke(EventArgs.Empty);
    }
}