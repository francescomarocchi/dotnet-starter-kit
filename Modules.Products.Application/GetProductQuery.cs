using Core.Application.BusinessStrategy;
using Core.Application.Dispatcher;
using Modules.Products.Domain;

namespace Modules.Products.Application;

public record GetProductQuery(Guid ProductId, string Source = ProductStrategies.Fake)
    : IQuery<Product?>, IBusinessStrategyRequest
{
    public string StrategyContext => Source;
}

