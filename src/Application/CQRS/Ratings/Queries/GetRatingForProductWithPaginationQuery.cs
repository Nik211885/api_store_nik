using Application.Common;
using ApplicationCore.Entities.Ratings;
using MediatR;

namespace Application.CQRS.Ratings.Queries
{
    public record GetRatingForProductWithPaginationQuery(string ProductId, int PageNumber = 1, int PageSize = 1) : IRequest<PaginationEntity<Rating>>;
}
