using Core.Application.Dispatcher;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modules.Authentication.Application;

namespace Modules.Authentication;

public static class AuthenticationEndpoints
{
    public static void MapAuthenticationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("auth");

        group.MapPost("/login", async (LoginRequest request, InternalDispatcher dispatcher) =>
        {
            var result = await dispatcher.SendCommandAsync(new LoginCommand(request.Email, request.Password));
            return Results.Ok(result);
        });

        group.MapPost("/refresh", async (RefreshRequest request, InternalDispatcher dispatcher) =>
        {
            var result = await dispatcher.SendCommandAsync(new RefreshTokenCommand(request.RefreshToken));
            return Results.Ok(result);
        });
    }
}

public record LoginRequest(string Email, string Password);
public record RefreshRequest(string RefreshToken);
