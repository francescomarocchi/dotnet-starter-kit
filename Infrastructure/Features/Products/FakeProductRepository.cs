using Core.Application.Features.Products;
using Core.Domain;

namespace Infrastructure.Features.Products;

public class FakeProductRepository: IProductRepository
{
    public Task<Product?> GetProduct(Guid id)
    {
        return Task.FromResult<Product?>(new Product("Test", "Test", 10, Guid.NewGuid()));
    }
    
    public Task<Product> CreateProduct(Product product)
    {
        return Task.FromResult(product);
    }
}