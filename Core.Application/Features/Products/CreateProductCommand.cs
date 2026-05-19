using Core.Application.Dispatcher;
using Core.Domain;

namespace Core.Application.Features.Products;

public record CreateProductCommand(
    string Name, 
    string Description, 
    decimal Price, 
    Guid CategoryId
) : ICommand<Product>;