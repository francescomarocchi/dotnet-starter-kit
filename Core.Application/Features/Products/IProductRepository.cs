using Core.Domain;

namespace Core.Application.Features.Products;

public interface IProductRepository 
{
    Task<Product?> GetProduct(Guid id);
    Task<Product> CreateProduct(Product product);
}