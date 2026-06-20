using BuildingBlocks.Modularity;
using Core.Application;
using Core.Application.BusinessStrategy;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Products.Application;
using Modules.Products.Infrastructure;

namespace Modules.Products;

public sealed class ProductsModule : IModule
{
    private const string ProductsNamespace = "Modules.Products.Application";

    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddCommandAndQueryHandlersFromAssembly(
            typeof(GetProductHandler).Assembly,
            namespacePrefix: ProductsNamespace);
        services.AddBusinessStrategiesFromAssembly(
            typeof(GetProductHandler).Assembly,
            namespacePrefix: ProductsNamespace);
        services.AddProductsInfrastructure(configuration);
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapProductsEndpoints();
    }
}
