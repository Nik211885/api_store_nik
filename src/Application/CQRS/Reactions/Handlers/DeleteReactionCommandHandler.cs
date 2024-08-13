using Application.Common.ResultTypes;
using Application.CQRS.Reactions.Commands;
using Application.CQRS.Reactions.Queries;
using Application.Interface;
using ApplicationCore.Entities.Ratings;
using MediatR;

namespace Application.CQRS.Reactions.Handlers
{
    public class DeleteReactionCommandHandler : IRequestHandler<DeleteReactionCommand, IResult>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly ISender _sender;
        public DeleteReactionCommandHandler(IStoreNikDbContext dbContext, ISender sender)
        {
            _dbContext = dbContext;
            _sender = sender;
        }
        public async Task<IResult> Handle(DeleteReactionCommand request, CancellationToken cancellationToken)
        {
            var reaction = await _sender.Send(new GetReactionByIdQuery(request.ReactionId), cancellationToken);
            if(reaction is null)
            {
                return FResult.NotFound(request.ReactionId, nameof(Reaction));
            }
            _dbContext.Reactions.Remove(reaction);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return FResult.Success();
        }
    }
}
