using MediatR;

namespace Application.CQRS.Reactions.Queries
{
    public record GetLikeAndUnLikeForRatingQuery(string RatingId) : IRequest<int[]>;
}
