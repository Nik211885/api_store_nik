using Application.Common.ResultTypes;
using MediatR;

namespace Application.CQRS.Reactions.Commands
{
    public record UpdateReactionCommand(string Id) : IRequest<Result>;
}
