using Application.Common.ResultTypes;
using MediatR;

namespace Application.Reactions.Commands
{
    public record DeleteReactionCommand(string ReactionId) : IRequest<Result>;
}
