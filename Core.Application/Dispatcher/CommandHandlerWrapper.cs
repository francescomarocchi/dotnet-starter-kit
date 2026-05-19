using Microsoft.Extensions.DependencyInjection;

namespace Core.Application.Dispatcher;

internal abstract class CommandHandlerWrapper<TResult>
{
    public abstract Task<TResult> HandleAsync(ICommand<TResult> command, IServiceProvider provider);
}

internal class CommandHandlerWrapperImpl<TCommand, TResult> : CommandHandlerWrapper<TResult>
    where TCommand : ICommand<TResult>
{
    public override async Task<TResult> HandleAsync(ICommand<TResult> command, IServiceProvider provider)
    {
        var handler = provider.GetRequiredService<ICommandHandler<TCommand, TResult>>();
        return await handler.HandleAsync((TCommand)command);
    }
}