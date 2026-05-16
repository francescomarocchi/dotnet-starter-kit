using Microsoft.Extensions.DependencyInjection;

namespace Core.Application;

internal abstract class QueryHandlerWrapper<TResult>
{
    public abstract Task<TResult> HandleAsync(IQuery<TResult> query, IServiceProvider provider);
}

internal class QueryHandlerWrapperImpl<TQuery, TResult> : QueryHandlerWrapper<TResult>    
    where TQuery : IQuery<TResult>
{
    public override Task<TResult> HandleAsync(IQuery<TResult> query, IServiceProvider provider)
    {
        var handler = provider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
        return handler.HandleAsync((TQuery)query);
    }
}