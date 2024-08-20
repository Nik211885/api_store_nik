using Application.CQRS.Products.Queries;
using Application.CQRS.Ratings.Queries;
using Application.DTOs.Reponse;
using Application.Interface;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Ratings.Handlers
{
    public class GetStatisticsRatingForProductQueryHandler
        : IRequestHandler<GetStatisticsRatingForProductQuery, IEnumerable<StatisticalRatingDTO>>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly ISender _sender;
        public GetStatisticsRatingForProductQueryHandler(IStoreNikDbContext dbContext,ISender sender)
        {
            _sender = sender;
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<StatisticalRatingDTO>> Handle(GetStatisticsRatingForProductQuery request, CancellationToken cancellationToken)
        {
            //Check product has exits
            var isProduct = await _sender.Send(new IsProductHasExitsQuery(request.ProductId),cancellationToken);
            if (!isProduct) throw new NotFoundException("Error",$"Don't find product has id is {request.ProductId}");
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
