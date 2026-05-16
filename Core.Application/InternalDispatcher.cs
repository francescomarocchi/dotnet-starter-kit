namespace Core.Application;

public class InternalDispatcher(IServiceProvider serviceProvider)
{
    public Task<TResult> SendQueryAsync<TResult>(IQuery<TResult> query)
    {
        var wrapperType = typeof(QueryHandlerWrapperImpl<,>)
            .MakeGenericType(query.GetType(), typeof(TResult));

        var wrapper = (QueryHandlerWrapper<TResult>)Activator.CreateInstance(wrapperType)!;

        return wrapper.HandleAsync(query, serviceProvider);
    }

    public Task<TResult> SendCommandAsync<TResult>(ICommand<TResult> command)
    {
        var wrapperType = typeof(CommandHandlerWrapperImpl<,>)
            .MakeGenericType(command.GetType(), typeof(TResult));

        var wrapper = (CommandHandlerWrapper<TResult>)Activator.CreateInstance(wrapperType)!;

        return wrapper.HandleAsync(command, serviceProvider);
    }
}