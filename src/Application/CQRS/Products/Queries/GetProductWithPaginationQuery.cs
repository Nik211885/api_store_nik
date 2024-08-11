using Application.Common;
using ApplicationCore.Entities.Products;
using MediatR;

namespace Application.CQRS.Products.Queries
{
    public record GetProductWithPaginationQuery(int PageNumber = 1, int PageSize = 20) :
        IRequest<PaginationEntity<Product>>;
}
