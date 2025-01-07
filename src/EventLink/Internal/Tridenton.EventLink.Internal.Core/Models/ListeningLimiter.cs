using Microsoft.Extensions.Options;

namespace Tridenton.EventLink.Internal.Core.Models;

public sealed class ListeningLimiter
{
    private ulong _current;
    
    private readonly EventLinkOptions _options;

    public ListeningLimiter(IOptions<EventLinkOptions> options)
    {
        _options = options.Value;
        _current = 0;
    }

    public ValueTask<bool> IsLimitExceededAsync()
    {
        var exceeded = !_options.Limitless && _current == _options.Limit;

        return ValueTask.FromResult(exceeded);
    }

    public ValueTask IncrementAsync()
    {
        if (_options.Limitless)
        {
            return ValueTask.CompletedTask;
        }
        
        Interlocked.Increment(ref _current);
        
        return ValueTask.CompletedTask;
    }
}