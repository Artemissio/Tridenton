namespace Tridenton.EventLink.Internal.Application.Core.Services.Internal;

internal sealed class ListeningLimiter : IListeningLimiter
{
    private readonly IEventLinkSettingsProvider _optionsProvider;
    private ulong _current;

    public ListeningLimiter(IEventLinkSettingsProvider optionsProvider)
    {
        _optionsProvider = optionsProvider;
        _current = 0;
    }

    public ValueTask<bool> IsLimitExceededAsync()
    {
        var options = _optionsProvider.GetSettings();
        
        var exceeded = !options.Limitless && _current == options.Limit;

        return ValueTask.FromResult(exceeded);
    }

    public ValueTask IncrementAsync()
    {
        var options = _optionsProvider.GetSettings();
        
        if (options.Limitless)
        {
            return ValueTask.CompletedTask;
        }
        
        Interlocked.Increment(ref _current);
        
        return ValueTask.CompletedTask;
    }
}