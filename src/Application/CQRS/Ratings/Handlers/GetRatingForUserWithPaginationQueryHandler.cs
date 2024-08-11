using Application.Common;
using Application.CQRS.Ratings.Queries;
using Application.Interface;
using ApplicationCore.Entities.Ratings;
using MediatR;

namespace Application.CQRS.Ratings.Handlers
{
    public class GetRatingForUserWithPaginationQueryHandler
        : IRequestHandler<GetRatingForUserWithPaginationQuery, PaginationEntity<Rating>>
    {
        private readonly IStoreNikDbContext _dbContext;
        public GetRatingForUserWithPaginationQueryHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<PaginationEntity<Rating>> Handle(GetRatingForUserWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var query = from r in _dbContext.Ratings
                        where r.UserId.Equals(request.UserId)
                        select r;
            return await PaginationEntity<Rating>.CreatePaginationEntityAsync(query, request.PageNumber, request.PageSize);
        }
    }
}
