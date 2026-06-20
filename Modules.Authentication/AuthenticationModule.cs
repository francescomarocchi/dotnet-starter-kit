using BuildingBlocks.Modularity;
using Core.Application;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Authentication.Application;
using Modules.Authentication.Infrastructure;

namespace Modules.Authentication;

public sealed class AuthenticationModule : IModule
{
    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddCommandAndQueryHandlersFromAssembly(
            typeof(LoginCommandHandler).Assembly,
            namespacePrefix: "Modules.Authentication.Application");
        services.AddAuthenticationInfrastructure();
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapAuthenticationEndpoints();
    }
}
