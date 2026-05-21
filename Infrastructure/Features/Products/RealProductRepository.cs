using Core.Application.Features.Products;
using Core.Domain;

namespace Infrastructure.Features.Products;

public class RealProductRepository : IProductRepository
{
    public Task<Product?> GetProduct(Guid id)
    {
        return Task.FromResult<Product?>(new Product("Real Product", "From real repository", 100, Guid.NewGuid()));
    }

    public Task<Product> CreateProduct(Product product)
    {
        return Task.FromResult(product);
    }
}

