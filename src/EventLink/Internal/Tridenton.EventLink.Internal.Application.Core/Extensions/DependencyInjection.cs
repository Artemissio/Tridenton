using Microsoft.Extensions.DependencyInjection;
using Tridenton.EventLink.Internal.Application.Core.Services.Internal;

namespace Tridenton.EventLink.Internal.Application.Core.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddEventLink(this IServiceCollection services)
    {
        services.AddSingleton<IEventLinkSettingsProvider, EventLinkSettingsProvider>();
        services.AddSingleton<IEventsStream, EventsStream>();
        services.AddSingleton<IEventsErrorsRepository, EventsErrorsRepository>();
        services.AddSingleton<IListeningLimiter, ListeningLimiter>();
        
        return services;
    }
}