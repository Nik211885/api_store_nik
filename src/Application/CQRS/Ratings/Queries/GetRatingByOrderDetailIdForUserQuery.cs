using Application.DTOs.Reponse;
using MediatR;

namespace Application.CQRS.Ratings.Queries
{
    public record GetRatingByOrderDetailIdForUserQuery(string UserId, string OrderDetailId) : IRequest<RatingReponse?>;
}
