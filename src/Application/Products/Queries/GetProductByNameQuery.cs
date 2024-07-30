using ApplicationCore.Entities.Products;
using MediatR;

namespace Application.Products.Queries
{
    public record GetProductByNameQuery(string name) : IRequest<Product>;
}
