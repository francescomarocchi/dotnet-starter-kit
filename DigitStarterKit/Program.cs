using System.Text.Json.Serialization;
using BuildingBlocks.Modularity;
using Modules.Authentication;
using Modules.Products;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddOpenApi();
builder.Services.AddModules(
    builder.Configuration,
    typeof(AuthenticationModule).Assembly,
    typeof(ProductsModule).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MapModules();

app.Run();