using Application.Common.ResultTypes;
using MediatR;

namespace Application.CQRS.Reactions.Commands
{
    public record CreateReactionCommand(bool Like, string RatingId) : IRequest<Result>;
}
