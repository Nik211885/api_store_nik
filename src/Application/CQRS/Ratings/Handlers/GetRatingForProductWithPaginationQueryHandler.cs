using Application.Common;
using Application.CQRS.Ratings.Queries;
using Application.Interface;
using ApplicationCore.Entities.Ratings;
using MediatR;

namespace Application.CQRS.Ratings.Handlers
{
    public class GetRatingForProductWithPaginationQueryHandler : IRequestHandler<GetRatingForProductWithPaginationQuery, PaginationEntity<Rating>>
    {
        private readonly IStoreNikDbContext _dbContext;
        public GetRatingForProductWithPaginationQueryHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<PaginationEntity<Rating>> Handle(GetRatingForProductWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var queryRatings = from rating in _dbContext.Ratings
                               where rating.ProductId.Equals(request.ProductId)
                               select rating;
            var pagination = await PaginationEntity<Rating>.CreatePaginationEntityAsync(queryRatings, request.PageNumber, request.PageSize);
            // Want to information all about pagination render it fe
            return pagination;
        }
    }
}
