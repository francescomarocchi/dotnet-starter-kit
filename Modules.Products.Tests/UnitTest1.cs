using Core.Application.Dispatcher;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Products.Application;
using Modules.Products.Domain;

namespace Modules.Products.Tests;

public class ProductsModuleTests
{
    [Fact]
    public async Task RegisterServices_ResolvesFakeProductQueryPath()
    {
        var services = CreateServices();

        await using var scope = services.BuildServiceProvider().CreateAsyncScope();
        var handler = scope.ServiceProvider.GetRequiredService<IQueryHandler<GetProductQuery, Product?>>();

        var result = await handler.HandleAsync(new GetProductQuery(Guid.NewGuid()));

        Assert.NotNull(result);
        Assert.Equal("Fake Product", (string)result.Name);
    }

    [Fact]
    public async Task RegisterServices_ResolvesRealProductQueryPath()
    {
        var services = CreateServices();

        await using var scope = services.BuildServiceProvider().CreateAsyncScope();
        var commandHandler = scope.ServiceProvider.GetRequiredService<ICommandHandler<CreateProductCommand, Product>>();
        var queryHandler = scope.ServiceProvider.GetRequiredService<IQueryHandler<GetProductQuery, Product?>>();

        var created = await commandHandler.HandleAsync(new CreateProductCommand(
            "Real Product",
            "From real repository",
            100,
            Guid.NewGuid()));

        var result = await queryHandler.HandleAsync(new GetProductQuery(created.Id, ProductStrategies.Real));

        Assert.NotNull(result);
        Assert.Equal("Real Product", (string)result.Name);
        Assert.Equal(created.Id, result.Id);
    }

    [Fact]
    public async Task RegisterServices_ResolvesCreateProductCommandPath()
    {
        var services = CreateServices();

        await using var scope = services.BuildServiceProvider().CreateAsyncScope();
        var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<CreateProductCommand, Product>>();

        var result = await handler.HandleAsync(new CreateProductCommand("Notebook", "Simple", 9.99m, Guid.NewGuid()));

        Assert.Equal("Notebook", (string)result.Name);
        Assert.NotEqual(Guid.Empty, result.Id);
    }

    private static ServiceCollection CreateServices()
    {
        var services = new ServiceCollection();
        var module = new global::Modules.Products.ProductsModule();
        module.RegisterServices(services, new ConfigurationBuilder().Build());
        return services;
    }
}
