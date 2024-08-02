using Application.Common.ResultTypes;
using Application.Interface;
using Application.Mappings;
using Application.Reactions.Commands;
using ApplicationCore.Entities.Ratings;
using MediatR;

namespace Application.Reactions.Handlers
{
    public class CreateReactionCommandHandler : MediatR.IRequestHandler<CreateReactionCommand, Result>
    {
        private readonly IStoreNikDbContext _dbContext;
        public CreateReactionCommandHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Result> Handle(CreateReactionCommand request, CancellationToken cancellationToken)
        {
            var reaction = Mapping<CreateReactionCommand, Reaction>.CreateMap().Map<Reaction>(request);
            _dbContext.Reactions.Add(reaction);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return FResult.Success();
        }
    }
}
