using ApplicationCore.Entities.Ratings;
using MediatR;

namespace Application.CQRS.Reactions.Queries
{
    public record GetReactionsByUserIdQuery(string UserId) : IRequest<IEnumerable<Reaction>?>;
}
