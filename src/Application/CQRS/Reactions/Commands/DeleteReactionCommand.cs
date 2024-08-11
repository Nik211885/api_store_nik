using Application.Common.ResultTypes;
using MediatR;

namespace Application.CQRS.Reactions.Commands
{
    public record DeleteReactionCommand(string ReactionId) : IRequest<Result>;
}
