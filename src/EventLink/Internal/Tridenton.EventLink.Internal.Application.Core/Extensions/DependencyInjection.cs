using System.Threading.Channels;
using Microsoft.Extensions.DependencyInjection;
using Tridenton.EventLink.Internal.Application.Core.Services.Internal;

namespace Tridenton.EventLink.Internal.Application.Core.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddEventLink(this IServiceCollection services)
    {
        var channel = Channel.CreateUnbounded<DataChangeEvent>();

        services.AddSingleton(channel);
        services.AddSingleton<IEventLinkSettingsProvider, EventLinkSettingsProvider>();
        services.AddSingleton<IEventsStream, EventsStream>();
        services.AddSingleton<IListeningLimiter, ListeningLimiter>();

        services.AddSingleton<EventsFilter, MainFilter>();
        
        services.AddScoped<IEventsErrorsRepository, EventsErrorsRepository>();

        services.AddDbContext<ApplicationDbContext>();
        
        return services;
    }
}