using Application.Common;
using Application.DTOs.Reponse;
using MediatR;

namespace Application.CQRS.Products.Queries
{
    public record GetProductWithPaginationQuery(int PageNumber = 1, int PageSize = 20) :
        IRequest<PaginationEntity<ProductDashboardReponse>>;
}
