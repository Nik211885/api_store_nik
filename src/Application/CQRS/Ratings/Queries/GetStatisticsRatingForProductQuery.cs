using Application.DTOs.Reponse;
using MediatR;

namespace Application.CQRS.Ratings.Queries
{
    public record GetStatisticsRatingForProductQuery(string ProductId) : IRequest<IEnumerable<StatisticalRatingDTO>>;
}
