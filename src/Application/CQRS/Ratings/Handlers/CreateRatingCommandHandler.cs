using Application.Common.ResultTypes;
using Application.CQRS.Ratings.Commands;
using Application.Interface;
using ApplicationCore.Entities.Ratings;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Ratings.Handlers
{
    //User just have one rating and just create don't delete and update rating 
    public class CreateRatingCommandHandler : IRequestHandler<CreateRatingCommand, IResult>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateRatingCommandHandler(IStoreNikDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<IResult> Handle(CreateRatingCommand request, CancellationToken cancellationToken)
        {
            var rating = _mapper.Map<Rating>(request);
            _dbContext.Ratings.Add(rating);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return FResult.Success();
        }
    }
}
