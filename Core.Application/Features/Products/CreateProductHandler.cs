using Core.Application.Dispatcher;
using Core.Domain;

namespace Core.Application.Features.Products;

public class CreateProductHandler(IProductRepository productRepository) : ICommandHandler<CreateProductCommand, Product>
{
    public Task<Product> HandleAsync(CreateProductCommand command)
    {
        return productRepository.CreateProduct(new Product(
            command.Name, 
            command.Description, 
            command.Price, 
            command.CategoryId
        ));
    }
}