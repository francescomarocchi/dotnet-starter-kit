using System.Reflection;
using Core.Application.Dispatcher;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Application;

public static class DependencyInjection
{
    public static void AddCommandAndQueryHandlersFromAssembly(
        this IServiceCollection services,
        Assembly assembly,
        string? namespacePrefix = null)
    {
        var concreteTypes = assembly.GetTypes()
            .Where(t => t is { IsAbstract: false, IsInterface: false })
            .Where(t => namespacePrefix is null || (t.Namespace?.StartsWith(namespacePrefix, StringComparison.Ordinal) ?? false));

        RegisterHandlers(services, concreteTypes, typeof(ICommandHandler<,>));
        RegisterHandlers(services, concreteTypes, typeof(IQueryHandler<,>));

        services.AddScoped<InternalDispatcher>();
    }

    private static void RegisterHandlers(
        IServiceCollection services,
        IEnumerable<Type> concreteTypes,
        Type openGenericHandlerType)
    {
        foreach (var implementationType in concreteTypes)
        {
            var serviceInterfaces = implementationType.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == openGenericHandlerType);

            foreach (var serviceType in serviceInterfaces)
            {
                services.AddScoped(serviceType, implementationType);
            }
        }
    }
}

