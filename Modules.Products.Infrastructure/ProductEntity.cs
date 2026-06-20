using Modules.Products.Domain;

namespace Modules.Products.Infrastructure;

public sealed class ProductEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }

    public Product ToDomain() => new(Id, Name, Description, Price, CategoryId);

    public static ProductEntity FromDomain(Product product) => new()
    {
        Id = product.Id == Guid.Empty ? Guid.NewGuid() : product.Id,
        Name = product.Name,
        Description = product.Description,
        Price = product.Price,
        CategoryId = product.CategoryId
    };
}

