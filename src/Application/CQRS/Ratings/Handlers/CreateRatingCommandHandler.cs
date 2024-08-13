using Application.Common.ResultTypes;
using Application.CQRS.Ratings.Commands;
using Application.Interface;
using Application.Mappings;
using ApplicationCore.Entities.Ratings;
using MediatR;

namespace Application.CQRS.Ratings.Handlers
{
    //User just have one rating and just create don't delete and update rating 
    public class CreateRatingCommandHandler : IRequestHandler<CreateRatingCommand, IResult>
    {
        private readonly IStoreNikDbContext _dbContext;
        public CreateRatingCommandHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IResult> Handle(CreateRatingCommand request, CancellationToken cancellationToken)
        {
            var rating = Mapping<CreateRatingCommand, Rating>.CreateMap().Map<Rating>(request);
            _dbContext.Ratings.Add(rating);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return FResult.Success();
        }
    }
}
