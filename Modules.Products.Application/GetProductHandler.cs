using Core.Application.BusinessStrategy;
using Core.Application.Dispatcher;
using Modules.Products.Domain;

namespace Modules.Products.Application;

public class GetProductHandler(BusinessStrategySelector<GetProductQuery, Product?> selector)
    : IQueryHandler<GetProductQuery, Product?>
{
    public Task<Product?> HandleAsync(GetProductQuery query)
    {
        return selector.SelectAndExecuteAsync(query);
    }
}

