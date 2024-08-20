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
            var reaction = _mapper.Map<Reaction>(request);
            _dbContext.Reactions.Add(reaction);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return FResult.Success();
        }
    }
}
