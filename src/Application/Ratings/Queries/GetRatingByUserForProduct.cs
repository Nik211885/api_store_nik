using Application.Common.ResultTypes;
using ApplicationCore.Entities.Ratings;
using MediatR;

namespace Application.Ratings.Queries
{
    public record GetRatingByUserForProduct(string UserId, string ProductId) : IRequest<Rating?>;
}
