using Core.Application;
using Core.Application.Queries;
using Core.Domain;

namespace Infrastructure;

public class GetProductHandler : IQueryHandler<GetProductQuery, Product?>
{
    public Task<Product?> HandleAsync(GetProductQuery query)
    {
        return Task.FromResult<Product?>(new Product("Test", "Test", 10, Guid.NewGuid()));
    }
}