using Application.CQRS.Ratings.Queries;
using Application.Interface;
using ApplicationCore.Entities.Ratings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Ratings.Handlers
{
    public class GetRatingByUserForProductHandler : IRequestHandler<GetRatingByUserForProduct, Rating?>
    {
        private readonly IStoreNikDbContext _dbContext;
        public GetRatingByUserForProductHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Rating?> Handle(GetRatingByUserForProduct request, CancellationToken cancellationToken)
        {
            var query = from r in _dbContext.Ratings
                        where r.UserId.Equals(request.UserId)
                                && r.ProductId.Equals(request.ProductId)
                        select r;
            return await query.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
