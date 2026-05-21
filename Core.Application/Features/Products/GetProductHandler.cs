using Core.Application.BusinessStrategy;
using Core.Application.Dispatcher;
using Core.Domain;

namespace Core.Application.Features.Products;

public class GetProductHandler(BusinessStrategySelector<GetProductQuery, Product?> selector)
    : IQueryHandler<GetProductQuery, Product?>
{
    public Task<Product?> HandleAsync(GetProductQuery query)
    {
        return selector.SelectAndExecuteAsync(query);
    }
}