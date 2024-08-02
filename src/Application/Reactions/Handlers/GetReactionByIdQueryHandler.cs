using Application.Interface;
using Application.Reactions.Queries;
using ApplicationCore.Entities.Ratings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Reactions.Handlers
{
    public class GetReactionByIdQueryHandler : MediatR.IRequestHandler<GetReactionByIdQuery, Reaction?>
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
