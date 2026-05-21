using Core.Application;
using Core.Application.Dispatcher;
using Core.Application.Features.Products;

namespace DigitStarterKit.Api;

public static class OrderEndpointsExtensions
{
    public static void MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/products");
        group.MapGet("/{id:guid}", async (Guid id, string? source, InternalDispatcher dispatcher) =>
        {
            var result = await dispatcher.SendQueryAsync(new GetProductQuery(id, source ?? ProductStrategies.Fake));
            return Results.Ok(result);
        });

        group.MapPost("/", async (CreateProductCommand command, InternalDispatcher dispatcher) =>
        {
            var result = await dispatcher.SendCommandAsync(command);
            return Results.Ok(result);
        });
    }
}



