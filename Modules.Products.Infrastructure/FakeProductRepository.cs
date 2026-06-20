using Modules.Products.Application;
using Modules.Products.Domain;

namespace Modules.Products.Infrastructure;

public class FakeProductRepository : IProductRepository
{
    public Task<Product?> GetProduct(Guid id)
    {
        return Task.FromResult<Product?>(new Product(
            id,
            "Fake Product",
            "From fake repository",
            10,
            Guid.NewGuid()));
    }

    public Task<Product> CreateProduct(Product product)
    {
        return Task.FromResult(product.Id == Guid.Empty ? product with { Id = Guid.NewGuid() } : product);
    }
}

