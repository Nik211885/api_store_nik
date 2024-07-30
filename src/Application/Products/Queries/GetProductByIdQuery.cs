using ApplicationCore.Entities.Products;
using MediatR;

namespace Application.Products.Queries
{
    public record class GetProductByIdQuery(string Id) : IRequest<Product?>;
}
