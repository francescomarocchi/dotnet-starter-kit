using Core.Application.Dispatcher;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modules.Products.Application;

namespace Modules.Products;

public static class ProductsEndpoints
{
    public static void MapProductsEndpoints(this IEndpointRouteBuilder app)
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
