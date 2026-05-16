using Core.Application;
using Core.Application.Commands;
using Core.Domain;

namespace Infrastructure;

public class CreateProductHandler : ICommandHandler<CreateProductCommand, Product>
{
    public Task<Product> HandleAsync(CreateProductCommand command)
    {
        return Task.FromResult<Product>(new Product(
                command.Name, 
                command.Description, 
                command.Price, 
                command.CategoryId
            )
        );
    }
}