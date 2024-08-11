using ApplicationCore.Entities.Ratings;
using MediatR;

namespace Application.CQRS.Ratings.Queries
{
    public record GetRatingByIdQuery(string Id) : IRequest<Rating?>;
}
