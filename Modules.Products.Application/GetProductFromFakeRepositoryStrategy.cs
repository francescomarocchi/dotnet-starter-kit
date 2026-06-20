using Core.Application.BusinessStrategy;
using Microsoft.Extensions.DependencyInjection;
using Modules.Products.Domain;

namespace Modules.Products.Application;

[BusinessStrategy(ProductStrategies.Fake)]
public sealed class GetProductFromFakeRepositoryStrategy(IServiceProvider serviceProvider)
    : IBusinessStrategy<GetProductQuery, Product?>
{
    public bool CanHandle(GetProductQuery request)
        => string.Equals(request.Source, ProductStrategies.Fake, StringComparison.OrdinalIgnoreCase);

    public Task<Product?> ExecuteAsync(GetProductQuery request, CancellationToken cancellationToken = default)
        => serviceProvider.GetRequiredKeyedService<IProductRepository>(ProductStrategies.Fake)
            .GetProduct(request.ProductId);
}

