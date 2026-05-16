using Core.Domain;

namespace Core.Application.Commands;

public record CreateProductCommand(
    string Name, 
    string Description, 
    decimal Price, 
    Guid CategoryId
) : ICommand<Product>;