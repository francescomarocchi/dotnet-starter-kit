using Modules.Products.Application;
using Modules.Products.Domain;
using Microsoft.EntityFrameworkCore;

namespace Modules.Products.Infrastructure;

public class RealProductRepository(ProductsDbContext dbContext) : IProductRepository
{
    public async Task<Product?> GetProduct(Guid id)
    {
        var entity = await dbContext.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        return entity?.ToDomain();
    }

    public async Task<Product> CreateProduct(Product product)
    {
        var entity = ProductEntity.FromDomain(product);
        dbContext.Products.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.ToDomain();
    }
}

