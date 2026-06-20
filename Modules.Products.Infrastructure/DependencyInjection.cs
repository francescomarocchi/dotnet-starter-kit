using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Modules.Products.Application;

namespace Modules.Products.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddProductsInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ProductsDb");

        services.AddDbContext<ProductsDbContext>(options =>
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                options.UseInMemoryDatabase("products-dev");
                return;
            }

            options.UseNpgsql(connectionString);
        });

        services.AddHostedService<ProductsDbInitializerHostedService>();

        services.AddKeyedScoped<IProductRepository, RealProductRepository>(ProductStrategies.Real);
        services.AddKeyedSingleton<IProductRepository, FakeProductRepository>(ProductStrategies.Fake);
        services.AddScoped<IProductRepository, RealProductRepository>();
        return services;
    }
}

