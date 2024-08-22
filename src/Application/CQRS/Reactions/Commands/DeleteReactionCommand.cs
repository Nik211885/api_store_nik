using Application.Interface;
using MediatR;

namespace Application.CQRS.Reactions.Commands
{
    public record DeleteReactionCommand(string UserId, string RatingId) : IRequest<IResult>;
}
