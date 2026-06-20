using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Application.BusinessStrategy;

public static class DependencyInjection
{
    public static void AddBusinessStrategiesFromAssembly(
        this IServiceCollection services,
        Assembly assembly,
        string? namespacePrefix = null)
    {
        services.AddScoped(typeof(BusinessStrategySelector<,>));

        var businessStrategyTypes = assembly.GetTypes()
            .Where(t => t.GetCustomAttribute<BusinessStrategyAttribute>() != null)
            .Where(t => t is { IsAbstract: false, IsInterface: false })
            .Where(t => namespacePrefix is null || (t.Namespace?.StartsWith(namespacePrefix, StringComparison.Ordinal) ?? false));

        foreach (var strategyType in businessStrategyTypes)
        {
            var attribute = strategyType.GetCustomAttribute<BusinessStrategyAttribute>();
            if (attribute is null)
            {
                continue;
            }

            var strategyInterfaces = strategyType.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IBusinessStrategy<,>));

            foreach (var strategyInterface in strategyInterfaces)
            {
                services.AddKeyedScoped(strategyInterface, attribute.Context, strategyType);
            }
        }
    }
}

