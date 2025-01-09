using Microsoft.Extensions.Configuration;

namespace Tridenton.EventLink.Internal.Application.Core.Services.Internal;

internal sealed class EventLinkSettingsProvider : IEventLinkSettingsProvider
{
    private EventLinkSettings _settings;

    public EventLinkSettingsProvider(IConfiguration configuration)
    {
        _settings = configuration.GetSection("EventLink").Get<EventLinkSettings>()!;
    }
    
    public EventLinkSettings GetSettings()
    {
        return _settings;
    }

    public void SetSettings(EventLinkSettings settings)
    {
        _settings = settings;
    }
}