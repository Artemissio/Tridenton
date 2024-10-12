using Microsoft.Extensions.DependencyInjection;
using Tridenton.Core.Operations.Internal;

namespace Tridenton.Core.Operations;

public static class DependencyInjection
{
    public static IServiceCollection AddTridentonOperationFlows(this IServiceCollection services, Action<OperationsFlowsOptionsBuilder>? builder = null)
    {
        var options = new OperationsFlowsOptionsBuilder();

        builder?.Invoke(options);

        services.AddSingleton(options);
        services.AddSingleton<IOperationsFlowsManager, OperationsFlowsManager>();

        return services;
    }
}