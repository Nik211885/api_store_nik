using Application.Common.ResultTypes;
using Application.Interface;
using Application.Reactions.Commands;
using Application.Reactions.Queries;
using MediatR;

namespace Application.Reactions.Handlers
{
    public class UpdateReactionCommandHandler : IRequestHandler<UpdateReactionCommand, Result>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly ISender _sender;
        public UpdateReactionCommandHandler(IStoreNikDbContext dbContext, ISender sender)
        {
            _dbContext = dbContext;
            _sender = sender;
        }
        public async Task<Result> Handle(UpdateReactionCommand request, CancellationToken cancellationToken)
        {
            var reaction = await _sender.Send(new GetReactionByIdQuery(request.Id),cancellationToken);
            if(reaction is null)
            {
                return FResult.Failure(FErrors.NotFound(request.Id));
            }
            reaction.Like = !reaction.Like;
            _dbContext.Reactions.Update(reaction);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return FResult.Success();
        }
    }
}
