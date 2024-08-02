using ApplicationCore.Entities.Ratings;
using MediatR;

namespace Application.Reactions.Queries
{
    public record class GetReactionByIdQuery(string Id) : IRequest<Reaction?>;
}
