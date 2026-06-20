using Core.Application.Dispatcher;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Authentication.Application;

namespace Modules.Authentication.Tests;

public class AuthenticationModuleTests
{
    [Fact]
    public async Task RegisterServices_ResolvesLoginCommandHandler()
    {
        var services = CreateServices();

        await using var scope = services.BuildServiceProvider().CreateAsyncScope();
        var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<LoginCommand, AuthenticationResult>>();

        var result = await handler.HandleAsync(new LoginCommand("admin@test.com", "password123"));

        Assert.NotNull(result.AccessToken);
        Assert.NotNull(result.RefreshToken);
    }

    [Fact]
    public async Task RegisterServices_ResolvesRefreshCommandHandler()
    {
        var services = CreateServices();

        await using var scope = services.BuildServiceProvider().CreateAsyncScope();
        var loginHandler = scope.ServiceProvider.GetRequiredService<ICommandHandler<LoginCommand, AuthenticationResult>>();
        var refreshHandler = scope.ServiceProvider.GetRequiredService<ICommandHandler<RefreshTokenCommand, AuthenticationResult>>();

        var login = await loginHandler.HandleAsync(new LoginCommand("admin@test.com", "password123"));
        var refresh = await refreshHandler.HandleAsync(new RefreshTokenCommand(login.RefreshToken!));

        Assert.NotNull(refresh.AccessToken);
        Assert.NotNull(refresh.RefreshToken);
    }

    private static ServiceCollection CreateServices()
    {
        var services = new ServiceCollection();
        var module = new global::Modules.Authentication.AuthenticationModule();
        module.RegisterServices(services, new ConfigurationBuilder().Build());
        return services;
    }
}
