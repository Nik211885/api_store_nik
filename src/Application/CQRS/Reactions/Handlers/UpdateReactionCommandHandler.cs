using Application.Common.ResultTypes;
using Application.CQRS.Reactions.Commands;
using Application.CQRS.Reactions.Queries;
using Application.Interface;
using ApplicationCore.Entities.Ratings;
using MediatR;

namespace Application.CQRS.Reactions.Handlers
{
    public class UpdateReactionCommandHandler : IRequestHandler<UpdateReactionCommand, IResult>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly ISender _sender;
        public UpdateReactionCommandHandler(IStoreNikDbContext dbContext, ISender sender)
        {
            _dbContext = dbContext;
            _sender = sender;
        }
        public async Task<IResult> Handle(UpdateReactionCommand request, CancellationToken cancellationToken)
        {
            var reaction = await _sender.Send(new GetReactionByIdQuery(request.Id), cancellationToken);
            if (reaction is null)
            {
                return FResult.NotFound(request.Id,nameof(Reaction));
            }
            reaction.Like = !reaction.Like;
            _dbContext.Reactions.Update(reaction);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return FResult.Success();
        }
    }
}
