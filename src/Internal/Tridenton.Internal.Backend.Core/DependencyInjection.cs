using Microsoft.Extensions.DependencyInjection;
using Tridenton.Internal.Backend.Core.Models;

namespace Tridenton.Internal.Backend.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddTridentonBackendCore(this IServiceCollection services, Action<BackendOptions> setupAction)
    {
        var backendOptions = new BackendOptions();
        
        setupAction.Invoke(backendOptions);
        
        services.AddSingleton(backendOptions);
        
        return services;
    }
}