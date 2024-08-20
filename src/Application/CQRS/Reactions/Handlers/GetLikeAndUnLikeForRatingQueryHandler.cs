using Application.CQRS.Reactions.Queries;
using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Reactions.Handlers
{
    public class GetLikeAndUnLikeForRatingQueryHandler
        : IRequestHandler<GetLikeAndUnLikeForRatingQuery, int[]>
    {
        private readonly IStoreNikDbContext _dbContext;
        public GetLikeAndUnLikeForRatingQueryHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int[]> Handle(GetLikeAndUnLikeForRatingQuery request, CancellationToken cancellationToken)
        {
            var query = from r in _dbContext.Reactions
                        where r.RatingId.Equals(request.RatingId)
                        select r.Like;
            var totalReaction = await query.CountAsync(cancellationToken);
            var like = await query.Where(x => x).CountAsync(cancellationToken);
            var unLike = totalReaction - like;
            return [like, unLike];
        }
    }
}
