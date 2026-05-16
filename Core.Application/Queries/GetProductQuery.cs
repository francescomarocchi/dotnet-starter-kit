using Core.Domain;

namespace Core.Application.Queries;

public record GetProductQuery(Guid ProductId) : IQuery<Product?>;