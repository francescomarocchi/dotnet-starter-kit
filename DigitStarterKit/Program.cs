using System.Text.Json.Serialization;
using Core.Application;
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

//TODO: this should be done properly using a marker interface
builder.Services.AddSingleton<IIdentityService, FakeIdentityService>();
builder.Services.AddSingleton<IProductRepository, FakeProductRepository>();

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