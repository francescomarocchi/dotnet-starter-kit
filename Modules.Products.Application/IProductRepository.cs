using Modules.Products.Domain;

namespace Modules.Products.Application;

public interface IProductRepository
{
    Task<Product?> GetProduct(Guid id);
    Task<Product> CreateProduct(Product product);
}

