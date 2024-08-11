using ApplicationCore.Entities.Ratings;
using MediatR;

namespace Application.CQRS.Reactions.Queries
{
    public record class GetReactionByIdQuery(string Id) : IRequest<Reaction?>;
}
