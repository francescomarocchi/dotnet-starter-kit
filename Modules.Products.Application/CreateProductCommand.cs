using Core.Application.Dispatcher;
using Modules.Products.Domain;

namespace Modules.Products.Application;

public record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    Guid CategoryId
) : ICommand<Product>;

