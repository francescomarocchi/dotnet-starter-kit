using Core.Application.Dispatcher;
using Core.Domain;

namespace Core.Application.Features.Products;

public record GetProductQuery(Guid ProductId) : IQuery<Product?>;