using Application.Common;
using ApplicationCore.Entities.Ratings;
using MediatR;

namespace Application.CQRS.Ratings.Queries
{
    public  record GetRatingForUserWithPaginationQuery(string UserId, int PageNumber, int PageSize) : IRequest<PaginationEntity<Rating>>;
}
