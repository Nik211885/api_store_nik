using Application.CQRS.Ratings.Queries;
using Application.Interface;
using ApplicationCore.Entities.Ratings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Ratings.Handlers
{
    public class GetRatingByIdQueryHandler : IRequestHandler<GetRatingByIdQuery, Rating?>
    {
        private readonly IStoreNikDbContext _dbContext;
        public GetRatingByIdQueryHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Rating?> Handle(GetRatingByIdQuery request, CancellationToken cancellationToken)
        {
            var query = from r in _dbContext.Ratings
                        where r.Id.Equals(request.Id)
                        select r;
            return await query.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
