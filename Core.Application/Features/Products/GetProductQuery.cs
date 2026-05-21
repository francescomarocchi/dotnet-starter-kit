using Core.Application.BusinessStrategy;
using Core.Application.Dispatcher;
using Core.Domain;

namespace Core.Application.Features.Products;

public record GetProductQuery(Guid ProductId, string Source = ProductStrategies.Fake)
	: IQuery<Product?>, IBusinessStrategyRequest
{
	public string StrategyContext => Source;
}
