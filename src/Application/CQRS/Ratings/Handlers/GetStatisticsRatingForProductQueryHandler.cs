using Application.CQRS.Products.Queries;
using Application.CQRS.Ratings.Queries;
using Application.DTOs.Reponse;
using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Ratings.Handlers
{
    public class GetStatisticsRatingForProductQueryHandler
        : IRequestHandler<GetStatisticsRatingForProductQuery, IEnumerable<StatisticalRatingDTO>>
    {
        private readonly IStoreNikDbContext _dbContext;
        public GetStatisticsRatingForProductQueryHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<StatisticalRatingDTO>> Handle(GetStatisticsRatingForProductQuery request, CancellationToken cancellationToken)
        {
            var query = from r in _dbContext.Ratings
                        where r.ProductId.Equals(request.ProductId)
                        orderby r.Start descending
                        group r.Start by r.Start into rn
                        select new StatisticalRatingDTO(rn.FirstOrDefault(), rn.Count());
            var result = await query.ToListAsync(cancellationToken);
            return result;
        }
    }
}
