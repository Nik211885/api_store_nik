using ApplicationCore.Entities.Products;
using MediatR;

namespace Application.CQRS.Products.Queries
{
    public record class GetProductByIdQuery(string Id) : IRequest<Product?>;
}
