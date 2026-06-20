using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Modularity;

public static class ModuleRegistrationExtensions
{
    public static IServiceCollection AddModules(
        this IServiceCollection services,
        IConfiguration configuration,
        params Assembly[] assemblies)
    {
        var modules = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => typeof(IModule).IsAssignableFrom(t) && t is { IsAbstract: false, IsInterface: false })
            .OrderBy(t => t.FullName, StringComparer.Ordinal)
            .Select(t => (IModule)Activator.CreateInstance(t)!)
            .ToList()
            .AsReadOnly();

        foreach (var module in modules)
        {
            module.RegisterServices(services, configuration);
        }

        services.AddSingleton<IReadOnlyCollection<IModule>>(modules);
        return services;
    }

    public static WebApplication MapModules(this WebApplication app)
    {
        var modules = app.Services.GetRequiredService<IReadOnlyCollection<IModule>>();

        foreach (var module in modules)
        {
            module.MapEndpoints(app);
        }

        return app;
    }
}
