using Application.Common;
using ApplicationCore.Entities.Products;
using MediatR;

namespace Application.CQRS.Products.Queries
{
    public record GetProductByNameWIthPaginationQuery(string NameProduct, int PageNumber = 1, int PageSize = 1) : IRequest<PaginationEntity<Product>>;
}
