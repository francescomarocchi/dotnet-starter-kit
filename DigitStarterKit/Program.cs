using Core.Application;
using DigitStarterKit.Api;
using Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var commandHandlers = typeof(GetProductHandler).Assembly.GetTypes()
    .Where(t => t.GetInterfaces().Any(i => 
        i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>)));

foreach (var handler in commandHandlers)
{
    var interfaceType = handler.GetInterfaces().First(i => 
        i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>));
    builder.Services.AddScoped(interfaceType, handler);
}

var queryHandlers = typeof(GetProductHandler).Assembly.GetTypes()
    .Where(t => t.GetInterfaces().Any(i => 
        i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)));

foreach (var handler in queryHandlers)
{
    var interfaceType = handler.GetInterfaces().First(i => 
        i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>));
    builder.Services.AddScoped(interfaceType, handler);
}

builder.Services.AddScoped<InternalDispatcher>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapOrderEndpoints();

app.Run();