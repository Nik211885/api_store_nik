using Application.CQRS.Ratings.Queries;
using Application.Interface;
using ApplicationCore.Entities.Ratings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Ratings.Handlers
{
    public class GetRatingsByUserHandler : IRequestHandler<GetRatingsByUser, IEnumerable<Rating>?>
    {
        private readonly IStoreNikDbContext _dbContext;
        public GetRatingsByUserHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Rating>?> Handle(GetRatingsByUser request, CancellationToken cancellationToken)
        {
            var query = from r in _dbContext.Ratings
                        where r.UserId.Equals(request.UserId)
                        select r;
            return await query.ToListAsync(cancellationToken);
        }
    }
}
