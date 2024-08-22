using Application.Common.ResultTypes;
using Application.CQRS.Reactions.Commands;
using Application.Interface;
using ApplicationCore.Entities.Ratings;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Reactions.Handlers
{
    public class CreateReactionCommandHandler : IRequestHandler<CreateReactionCommand, IResult>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateReactionCommandHandler(IStoreNikDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<IResult> Handle(CreateReactionCommand request, CancellationToken cancellationToken)
        {
            var reaction = new Reaction(request.Reaction.Like, request.Reaction.RatingId, request.UserId);
            _dbContext.Reactions.Add(reaction);
            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch(Exception ex)
            {
                return FResult.Failure("You can have one reaction in one rating");
            }
            return FResult.Success();
        }
    }
}
