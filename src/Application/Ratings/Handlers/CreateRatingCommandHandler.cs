using Application.Common.ResultTypes;
using Application.Interface;
using Application.Mappings;
using Application.Ratings.Commands;
using ApplicationCore.Entities.Ratings;
using MediatR;

namespace Application.Ratings.Handlers
{
    //User just have one rating and just create don't delete and update rating 
    public class CreateRatingCommandHandler : IRequestHandler<CreateRatingCommand, Result>
    {
        private readonly IStoreNikDbContext _dbContext;
        public CreateRatingCommandHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Result> Handle(CreateRatingCommand request, CancellationToken cancellationToken)
        {
            var rating = Mapping<CreateRatingCommand, Rating>.CreateMap().Map<Rating>(request);
            _dbContext.Ratings.Add(rating);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return FResult.Success();
        }
    }
}
