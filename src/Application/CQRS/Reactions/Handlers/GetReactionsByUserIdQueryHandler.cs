using Application.CQRS.Reactions.Queries;
using Application.Interface;
using ApplicationCore.Entities.Ratings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Reactions.Handlers
{
    public class GetReactionsByUserIdQueryHandler : IRequestHandler<GetReactionsByUserIdQuery, IEnumerable<Reaction>?>
    {
        private readonly IStoreNikDbContext _dbContext;
        public GetReactionsByUserIdQueryHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Reaction>?> Handle(GetReactionsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var reactionsQuery = from r in _dbContext.Reactions
                                 where r.UserId.Equals(request.UserId)
                                 select r;
            return await reactionsQuery.ToListAsync(cancellationToken);
        }
    }
}
