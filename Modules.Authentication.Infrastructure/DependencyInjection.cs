using Microsoft.Extensions.DependencyInjection;
using Modules.Authentication.Application;

namespace Modules.Authentication.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddAuthenticationInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IIdentityService, FakeIdentityService>();
        return services;
    }
}

