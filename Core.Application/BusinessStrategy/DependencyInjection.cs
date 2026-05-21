using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Application.BusinessStrategy;

public static class DependencyInjection
{
    public static void AddBusinessStrategies(this IServiceCollection services)
    {
        // Adds the BusinessStrategySelector as a scoped service, allowing it to be injected where needed.
        services.AddScoped(typeof(BusinessStrategySelector<,>));

        // Adds all BusinessStrategies as scoped services, keyed by the context they are associated with.
        var applicationAssembly = typeof(IBusinessStrategy<,>).Assembly;
        var businessStrategyTypes = applicationAssembly.GetTypes()
            .Where(t => t.GetCustomAttribute<BusinessStrategyAttribute>() != null &&
                        t is { IsAbstract: false, IsInterface: false });

        foreach (var strategyType in businessStrategyTypes)
        {
            var attribute = strategyType.GetCustomAttribute<BusinessStrategyAttribute>();
            if (attribute is null)
            {
                continue;
            }

            var contextKey = attribute.Context;

            var businessStrategyInterface = strategyType.GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IBusinessStrategy<,>));

            if (businessStrategyInterface is not null)
            {
                services.AddKeyedScoped(businessStrategyInterface, contextKey, strategyType);
            }
        }
    }
}