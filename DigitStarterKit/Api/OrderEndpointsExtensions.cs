using Core.Application;
using Core.Application.Commands;
using Core.Application.Queries;

namespace DigitStarterKit.Api;

public static class OrderEndpointsExtensions
{
    public static void MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id:guid}", async (Guid id, InternalDispatcher dispatcher) =>
        {
            var result = await dispatcher.SendQueryAsync(new GetProductQuery(id));
            return Results.Ok(result);
        });

        app.MapPost("/products", async (CreateProductCommand command, InternalDispatcher dispatcher) =>
        {
            var result = await dispatcher.SendCommandAsync(command);
            return Results.Ok(result);
        });
    }
}



