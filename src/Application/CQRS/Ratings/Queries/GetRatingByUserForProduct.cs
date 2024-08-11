using ApplicationCore.Entities.Ratings;
using MediatR;

namespace Application.CQRS.Ratings.Queries
{
    public record GetRatingByUserForProduct(string UserId, string ProductId) : IRequest<Rating?>;
}
