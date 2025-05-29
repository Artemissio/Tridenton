using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Tridenton.Internal.Core.Context;
using Tridenton.Internal.Core.Context.Internal;
using Tridenton.Internal.Core.CQRS;

namespace Tridenton.Internal.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddTridentonCore(this IServiceCollection services)
    {
        services.AddScoped<IRuntimeContextAccessor, DefaultRuntimeContextAccessor>();

        services.AddOrchestrator();
        
        return services;
    }

    private static IServiceCollection AddOrchestrator(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        var requests = assembly
                .GetTypes()
                .Where(t => t.IsAssignableFrom(typeof(ITridentonRequest)) || t.IsAssignableFrom(typeof(ITridentonRequest<>)))
                .ToArray();
        
        return services;
    }
}