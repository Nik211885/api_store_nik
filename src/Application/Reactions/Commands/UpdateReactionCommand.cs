using Application.Common.ResultTypes;
using MediatR;

namespace Application.Reactions.Commands
{
    public record UpdateReactionCommand(string Id) : IRequest<Result>;
}
