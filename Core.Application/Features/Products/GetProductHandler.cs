using Core.Application.Dispatcher;
using Core.Domain;

namespace Core.Application.Features.Products;

public class GetProductHandler(IProductRepository productRepository) : IQueryHandler<GetProductQuery, Product?>
{
    public Task<Product?> HandleAsync(GetProductQuery query)
    {
        return productRepository.GetProduct(query.ProductId);
    }
}