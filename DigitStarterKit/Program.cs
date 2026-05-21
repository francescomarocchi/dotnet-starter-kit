using System.Text.Json.Serialization;
using Core.Application;
using Core.Application.BusinessStrategy;
using Core.Application.Features.Authentication;
using Core.Application.Features.Products;
using DigitStarterKit.Api;
using Infrastructure.Features.Authentication;
using Infrastructure.Features.Products;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddOpenApi();

builder.Services.AddCommandAndQueryHandlers();
builder.Services.AddBusinessStrategies();

//TODO: this should be done properly using a marker interface
builder.Services.AddSingleton<IIdentityService, FakeIdentityService>();
builder.Services.AddKeyedSingleton<IProductRepository, RealProductRepository>(ProductStrategies.Real);
builder.Services.AddKeyedSingleton<IProductRepository, FakeProductRepository>(ProductStrategies.Fake);
builder.Services.AddSingleton<IProductRepository, RealProductRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapLoginEndpoints();
app.MapOrderEndpoints();

app.Run();