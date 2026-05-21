using Microsoft.Extensions.DependencyInjection;

namespace Core.Application.BusinessStrategy;

public class BusinessStrategySelector<TRequest, TResponse>(IServiceProvider serviceProvider)
    where TRequest : IBusinessStrategyRequest
{
    public async Task<TResponse> SelectAndExecuteAsync(TRequest request, CancellationToken cancellationToken = default)
    {
        // Resolve business strategies of the same TRequest, TResponse type of this BusinessStrategySelector to narrow
        // down the candidates. Then, use the CanHandle method to find the appropriate strategy for the given request.
        var candidates = serviceProvider.GetKeyedServices<IBusinessStrategy<TRequest, TResponse>>(request.StrategyContext);
        var businessStrategy = candidates.FirstOrDefault(s => s.CanHandle(request));

        if (businessStrategy is null)
        {
            throw new InvalidOperationException($"No business strategy found for request of type {typeof(TRequest).Name}");
        }

        return await businessStrategy.ExecuteAsync(request, cancellationToken);
    }
}