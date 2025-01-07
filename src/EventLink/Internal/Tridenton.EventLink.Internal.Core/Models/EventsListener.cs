using Tridenton.EventLink.SDK.Sources;

namespace Tridenton.EventLink.Internal.Core.Models;

public abstract class EventsListener<TSettings>
    where TSettings : ISourceSettingsMarker
{
    private readonly ListeningLimiter _limiter;

    protected EventsListener(ListeningLimiter limiter)
    {
        _limiter = limiter;
    }
}