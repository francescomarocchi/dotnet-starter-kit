using Core.Application.BusinessStrategy;
using Core.Application.Dispatcher;
using Core.Application.Features.Products;
using Core.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Application.Tests;

public class ProductStrategySelectionTests
{
    [Fact]
    public async Task GetProductHandler_UsesFakeStrategy_WhenSourceIsFake()
    {
        var services = CreateServices();

        await using var scope = services.BuildServiceProvider().CreateAsyncScope();
        var handler = scope.ServiceProvider.GetRequiredService<IQueryHandler<GetProductQuery, Product?>>();

        var result = await handler.HandleAsync(new GetProductQuery(Guid.NewGuid(), ProductStrategies.Fake));

        Assert.NotNull(result);
        Assert.Equal("fake", result!.Name);
    }

    [Fact]
    public async Task GetProductHandler_UsesRealStrategy_WhenSourceIsReal()
    {
        var services = CreateServices();

        await using var scope = services.BuildServiceProvider().CreateAsyncScope();
        var handler = scope.ServiceProvider.GetRequiredService<IQueryHandler<GetProductQuery, Product?>>();

        var result = await handler.HandleAsync(new GetProductQuery(Guid.NewGuid(), ProductStrategies.Real));

        Assert.NotNull(result);
        Assert.Equal("real", result!.Name);
    }

    [Fact]
    public async Task GetProductHandler_Throws_WhenSourceHasNoStrategy()
    {
        var services = CreateServices();

        await using var scope = services.BuildServiceProvider().CreateAsyncScope();
        var handler = scope.ServiceProvider.GetRequiredService<IQueryHandler<GetProductQuery, Product?>>();

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => handler.HandleAsync(new GetProductQuery(Guid.NewGuid(), "unknown")));
    }

    private static ServiceCollection CreateServices()
    {
        var services = new ServiceCollection();
        services.AddBusinessStrategies();
        services.AddScoped<IQueryHandler<GetProductQuery, Product?>, GetProductHandler>();
        services.AddKeyedScoped<IProductRepository, StubRealProductRepository>(ProductStrategies.Real);
        services.AddKeyedScoped<IProductRepository, StubFakeProductRepository>(ProductStrategies.Fake);
        services.AddScoped<IProductRepository, StubRealProductRepository>();
        return services;
    }

    private sealed class StubRealProductRepository : IProductRepository
    {
        public Task<Product?> GetProduct(Guid id)
            => Task.FromResult<Product?>(new Product("real", "real", 100, Guid.NewGuid()));

        public Task<Product> CreateProduct(Product product)
            => Task.FromResult(product);
    }

    private sealed class StubFakeProductRepository : IProductRepository
    {
        public Task<Product?> GetProduct(Guid id)
            => Task.FromResult<Product?>(new Product("fake", "fake", 10, Guid.NewGuid()));

        public Task<Product> CreateProduct(Product product)
            => Task.FromResult(product);
    }
}


