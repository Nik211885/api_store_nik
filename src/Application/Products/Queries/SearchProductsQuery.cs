using ApplicationCore.Entities.Products;
using MediatR;

namespace Application.Products.Queries
{
    public record SearchProductsQuery(string Search) : IRequest<IEnumerable<Product>?>;
}
