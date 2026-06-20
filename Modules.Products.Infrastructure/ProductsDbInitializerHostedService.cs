using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace Modules.Products.Infrastructure;
public sealed class ProductsDbInitializerHostedService(IServiceScopeFactory scopeFactory) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = scopeFactory.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ProductsDbContext>();
        await dbContext.Database.EnsureCreatedAsync(cancellationToken);
        if (await dbContext.Products.AnyAsync(cancellationToken))
        {
            return;
        }
        dbContext.Products.AddRange(
            new ProductEntity
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = "Desk Lamp",
                Description = "Warm light desk lamp",
                Price = 29.99m,
                CategoryId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")
            },
            new ProductEntity
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name = "Mechanical Keyboard",
                Description = "Tenkeyless mechanical keyboard",
                Price = 99.00m,
                CategoryId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb")
            });
        await dbContext.SaveChangesAsync(cancellationToken);
    }
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
