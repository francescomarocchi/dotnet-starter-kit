using Core.Application.BusinessStrategy;
using Microsoft.Extensions.DependencyInjection;
using Modules.Products.Domain;

namespace Modules.Products.Application;

[BusinessStrategy(ProductStrategies.Real)]
public sealed class GetProductFromRealRepositoryStrategy(IServiceProvider serviceProvider)
    : IBusinessStrategy<GetProductQuery, Product?>
{
    public bool CanHandle(GetProductQuery request)
        => string.Equals(request.Source, ProductStrategies.Real, StringComparison.OrdinalIgnoreCase);

    public Task<Product?> ExecuteAsync(GetProductQuery request, CancellationToken cancellationToken = default)
        => serviceProvider.GetRequiredKeyedService<IProductRepository>(ProductStrategies.Real)
            .GetProduct(request.ProductId);
}

