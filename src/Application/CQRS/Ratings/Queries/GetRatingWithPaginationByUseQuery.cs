using Application.Common;
using Application.DTOs.Reponse;
using MediatR;

namespace Application.CQRS.Ratings.Queries
{
    public record GetRatingWithPaginationByUseQuery(string UserId, int PageNumber = 1, int PageSize = 10) : IRequest<PaginationEntity<RatingWithProductIdReponse>>;
}
