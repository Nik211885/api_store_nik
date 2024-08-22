using Application.CQRS.Reactions.Queries;
using Application.Interface;
using ApplicationCore.Entities.Ratings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Reactions.Handlers
{
    public class GetReactionByUserForRatingQueryHandler
        : IRequestHandler<GetReactionByUserForRatingQuery, Reaction?>
    {
        private readonly IStoreNikDbContext _dbContext;
        public GetReactionByUserForRatingQueryHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Reaction?> Handle(GetReactionByUserForRatingQuery request, CancellationToken cancellationToken)
        {
            var query = from r in _dbContext.Reactions
                        where r.UserId.Equals(request.UserId)
                                && r.RatingId.Equals(request.RatingId)
                                select r;
            return await query.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
