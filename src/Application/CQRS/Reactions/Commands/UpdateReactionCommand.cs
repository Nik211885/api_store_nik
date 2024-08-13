using Application.Interface;
using MediatR;

namespace Application.CQRS.Reactions.Commands
{
    public record UpdateReactionCommand(string Id) : IRequest<IResult>;
}
