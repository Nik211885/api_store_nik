using Application.DTOs.Request;
using Application.Interface;
using MediatR;

namespace Application.CQRS.Reactions.Commands
{
    public record CreateReactionCommand(string UserId, ReactionViewModel Reaction) : IRequest<IResult>;
}
