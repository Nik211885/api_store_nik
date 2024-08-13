using Application.Interface;
using MediatR;

namespace Application.CQRS.Reactions.Commands
{
    public record CreateReactionCommand(bool Like, string RatingId) : IRequest<IResult>;
}
