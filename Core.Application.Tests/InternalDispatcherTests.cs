using Core.Application.Dispatcher;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Application.Tests;

public class InternalDispatcherTests
{
    [Fact]
    public async Task SendQueryAsync_ReturnsHandlerResult()
    {
        var services = new ServiceCollection();
        services.AddScoped<IQueryHandler<TestQuery, string>, TestQueryHandler>();

        await using var scope = services.BuildServiceProvider().CreateAsyncScope();
        var dispatcher = new InternalDispatcher(scope.ServiceProvider);

        var result = await dispatcher.SendQueryAsync(new TestQuery("42"));

        Assert.Equal("query:42", result);
    }

    [Fact]
    public async Task SendCommandAsync_ReturnsHandlerResult()
    {
        var services = new ServiceCollection();
        services.AddScoped<ICommandHandler<TestCommand, string>, TestCommandHandler>();

        await using var scope = services.BuildServiceProvider().CreateAsyncScope();
        var dispatcher = new InternalDispatcher(scope.ServiceProvider);

        var result = await dispatcher.SendCommandAsync(new TestCommand("Notebook"));

        Assert.Equal("created:Notebook", result);
    }

    [Fact]
    public async Task SendQueryAsync_ThrowsWhenHandlerMissing()
    {
        var services = new ServiceCollection();

        await using var scope = services.BuildServiceProvider().CreateAsyncScope();
        var dispatcher = new InternalDispatcher(scope.ServiceProvider);

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => dispatcher.SendQueryAsync(new MissingQuery()));
    }

    [Fact]
    public async Task SendCommandAsync_ThrowsWhenHandlerMissing()
    {
        var services = new ServiceCollection();

        await using var scope = services.BuildServiceProvider().CreateAsyncScope();
        var dispatcher = new InternalDispatcher(scope.ServiceProvider);

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => dispatcher.SendCommandAsync(new MissingCommand()));
    }

    private sealed record TestQuery(string Id) : IQuery<string>;

    private sealed class TestQueryHandler : IQueryHandler<TestQuery, string>
    {
        public Task<string> HandleAsync(TestQuery query) => Task.FromResult($"query:{query.Id}");
    }

    private sealed record TestCommand(string Name) : ICommand<string>;

    private sealed class TestCommandHandler : ICommandHandler<TestCommand, string>
    {
        public Task<string> HandleAsync(TestCommand command) => Task.FromResult($"created:{command.Name}");
    }

    private sealed record MissingQuery : IQuery<int>;

    private sealed record MissingCommand : ICommand<int>;
}
