using Core.Application.Dispatcher;
using Core.Application.Features.Products;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Application;

public static class DependencyInjection
{
    public static void AddCommandAndQueryHandlers(this IServiceCollection services)
    {
        var commandHandlers = typeof(GetProductHandler).Assembly.GetTypes()
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>)));

        foreach (var handler in commandHandlers)
        {
            var interfaceType = handler.GetInterfaces().First(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>));
            services.AddScoped(interfaceType, handler);
        }

        var queryHandlers = typeof(GetProductHandler).Assembly.GetTypes()
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)));

        foreach (var handler in queryHandlers)
        {
            var interfaceType = handler.GetInterfaces().First(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>));
            services.AddScoped(interfaceType, handler);
        }

        services.AddScoped<InternalDispatcher>();
    }
}