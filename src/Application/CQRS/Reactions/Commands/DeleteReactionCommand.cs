using Application.Interface;
using MediatR;

namespace Application.CQRS.Reactions.Commands
{
    public record DeleteReactionCommand(string ReactionId) : IRequest<IResult>;
}
