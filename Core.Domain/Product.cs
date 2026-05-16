namespace Core.Domain;

public record Product(
    string Name,
    string Description,
    decimal Price,
    Guid CategoryId
);