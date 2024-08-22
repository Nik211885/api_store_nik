using Application.Common.ResultTypes;
using Application.CQRS.Reactions.Commands;
using Application.CQRS.Reactions.Queries;
using Application.Interface;
using MediatR;

namespace Application.CQRS.Reactions.Handlers
{
    public class UpdateReactionCommandHandler
        : IRequestHandler<UpdateReactionCommand, IResult>
    {
        private readonly ISender _sender;
        private readonly IStoreNikDbContext _dbContext;
        public UpdateReactionCommandHandler(ISender sender, IStoreNikDbContext dbContext)
        {
            _sender = sender;
            _dbContext = dbContext;
        }
        public async Task<IResult> Handle(UpdateReactionCommand request, CancellationToken cancellationToken)
        {
            var reaction = await _sender.Send(new GetReactionByUserForRatingQuery(request.UserId, request.RatingId),cancellationToken);
           if(reaction is null)
            {
                return FResult.Failure($"You don't have reaction to rating has id {request.RatingId}");
            }
            reaction.Like = !reaction.Like;
            _dbContext.Reactions.Update(reaction);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return FResult.Success();
        }
    }
}
