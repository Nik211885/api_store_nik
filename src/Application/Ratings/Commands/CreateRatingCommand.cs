using Application.Common.ResultTypes;
using MediatR;

namespace Application.Ratings.Commands
{
    public record CreateRatingCommand(string UserId, 
        string ProductId,
        float Start,
        string? CommentRating) : IRequest<Result>;
}
