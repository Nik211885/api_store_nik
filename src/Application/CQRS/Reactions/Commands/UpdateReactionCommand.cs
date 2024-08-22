using Application.Interface;
using MediatR;

namespace Application.CQRS.Reactions.Commands
{
    public record UpdateReactionCommand(string UserId, string RatingId) : IRequest<IResult>;
}
