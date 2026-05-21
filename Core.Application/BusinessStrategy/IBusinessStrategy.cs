namespace Core.Application.BusinessStrategy;

public interface IBusinessStrategy<in TRequest, TResponse>
{
    bool CanHandle(TRequest request);
    Task<TResponse> ExecuteAsync(TRequest request, CancellationToken cancellationToken = default);
}