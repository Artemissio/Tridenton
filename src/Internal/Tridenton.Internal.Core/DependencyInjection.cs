using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Tridenton.Internal.Core.Context;
using Tridenton.Internal.Core.Context.Internal;
using Tridenton.Internal.Core.CQRS;
using Tridenton.Internal.Core.CQRS.Internal;

namespace Tridenton.Internal.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddTridentonCore(this IServiceCollection services)
    {
        services = services.AddHttpContextAccessor();
        
        services = services.AddScoped<IRuntimeContextAccessor, DefaultRuntimeContextAccessor>();

        services = services.AddOrchestrator();
        
        return services;
    }

    private static IServiceCollection AddOrchestrator(this IServiceCollection services)
    {
        var executingAssembly = Assembly
            .GetExecutingAssembly();
        
        var callingAssembly = Assembly
            .GetCallingAssembly();
        
        var entryAssembly = Assembly
            .GetEntryAssembly();
        
        var assemblyTypes = executingAssembly
            .GetTypes()
            .Concat(callingAssembly.GetTypes())
            .ToArray();

        if (entryAssembly is not null)
        {
            assemblyTypes = assemblyTypes
                .Concat(entryAssembly.GetTypes())
                .ToArray();
        }

        var requestHandlersTypes = assemblyTypes
            .Where(t => t != typeof(IRequestHandler))
            .Where(t => t != typeof(IRequestHandler<>))
            .Where(t => t != typeof(IRequestHandler<,>))
            .Where(t => t.IsAssignableTo(typeof(IRequestHandler)))
            .ToArray();

        var handlers = requestHandlersTypes
            .Select(t =>
            {
                var interfaceType = t
                    .GetInterfaces()
                    .First(i => i.IsGenericType);
                
                var requestType = interfaceType
                    .GetGenericArguments()[0];

                return new
                {
                    RequestType = requestType,
                    HandlerType = t,
                };
            })
            .ToDictionary(
                i => i.RequestType,
                i => i.HandlerType);

        foreach (var handler in handlers)
        {
            services = services.AddScoped(handler.Value);
            
            // var interfaceType = handler.Value
            //     .GetInterfaces()
            //     .First(i =>
            //         i.IsGenericType &&
            //         (i.GetGenericTypeDefinition() == typeof(IRequestHandler<>) ||
            //          i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)));
            //
            // services = services.AddScoped(interfaceType, handler.Value);
        }

        var orchestratorSetup = new OrchestratorSetup(handlers);
        
        services = services.AddSingleton(orchestratorSetup);

        services = services.AddSingleton<IOrchestrator, Orchestrator>();
        
        return services;
    }
}