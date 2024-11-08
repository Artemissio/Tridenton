using Microsoft.Extensions.DependencyInjection;
using Tridenton.Internal.Core.Context;
using Tridenton.Internal.Core.Context.Internal;

namespace Tridenton.Internal.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddTridentonCore(this IServiceCollection services)
    {
        services.AddScoped<IRuntimeContextAccessor, DefaultRuntimeContextAccessor>();
        
        return services;
    }
}