using Core.Application.BusinessStrategy;
using Core.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Application.Features.Products;

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


