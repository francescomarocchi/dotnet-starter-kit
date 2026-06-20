using Core.Application.Dispatcher;
using Modules.Products.Domain;

namespace Modules.Products.Application;

public class CreateProductHandler(IProductRepository productRepository) : ICommandHandler<CreateProductCommand, Product>
{
    public Task<Product> HandleAsync(CreateProductCommand command)
    {
        return productRepository.CreateProduct(new Product(
            Guid.NewGuid(),
            command.Name,
            command.Description,
            command.Price,
            command.CategoryId
        ));
    }
}

