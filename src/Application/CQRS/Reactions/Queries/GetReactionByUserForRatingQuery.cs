using ApplicationCore.Entities.Ratings;
using MediatR;

namespace Application.CQRS.Reactions.Queries
{
    public record GetReactionByUserForRatingQuery(string UserId, string RatingId) : IRequest<Reaction?>;
}
