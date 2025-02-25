using System.Threading.Channels;

namespace Tridenton.EventLink.Internal.Application.Core.Services.Internal;

/// <summary>
/// Default events stream implementation
/// </summary>
internal sealed class EventsStream : IEventsStream
{
    private readonly Channel<DataChangeEvent> _channel;
    
    public EventsStream(Channel<DataChangeEvent> channel)
    {
        _channel = channel;
    }
    
    public async ValueTask WriteAsync(DataChangeEvent @event)
    {
        await _channel.Writer.WriteAsync(@event);
    }

    public ValueTask<bool> WaitToReadAsync()
    {
        return _channel.Reader.WaitToReadAsync();
    }

    public async ValueTask<IEventsStreamingContext> ReadAsync()
    {
        var payload = await _channel.Reader.ReadAsync();

        var context = new EventsStreamingContext(payload);
            
        return context;
    }

    public void Dispose()
    {
        _channel.Writer.Complete();
    }
}