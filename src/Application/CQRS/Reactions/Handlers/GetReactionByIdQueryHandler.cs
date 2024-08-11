using Application.CQRS.Reactions.Queries;
using Application.Interface;
using ApplicationCore.Entities.Ratings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Reactions.Handlers
{
    public class GetReactionByIdQueryHandler : IRequestHandler<GetReactionByIdQuery, Reaction?>
    {
        private readonly IStoreNikDbContext _dbContext;
        public GetReactionByIdQueryHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Reaction?> Handle(GetReactionByIdQuery request, CancellationToken cancellationToken)
        {
            var queryReaction = from r in _dbContext.Reactions
                                where r.Id.Equals(request.Id)
                                select r;
            var reaction = await queryReaction.FirstOrDefaultAsync(cancellationToken);
            return reaction;
        }
    }
}
