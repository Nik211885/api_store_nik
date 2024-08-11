using ApplicationCore.Entities.Ratings;
using MediatR;

namespace Application.CQRS.Ratings.Queries
{
    public record GetRatingsByUser(string UserId) : IRequest<IEnumerable<Rating>?>;
}
