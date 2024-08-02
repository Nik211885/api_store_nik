using ApplicationCore.Entities.Ratings;
using MediatR;

namespace Application.Ratings.Queries
{
    public record GetRatingByIdQuery(string Id) : IRequest<Rating?>;
}
