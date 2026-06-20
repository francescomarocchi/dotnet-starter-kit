namespace Modules.Products.Domain;

public record Product(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    Guid CategoryId
);

