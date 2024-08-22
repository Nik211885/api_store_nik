using Application.Common.ResultTypes;
using Application.CQRS.Ratings.Queries;
using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Ratings.Handlers
{
    public class GetRatingByOrderDetailIdQueryHandler
        : IRequestHandler<GetRatingByOrderDetailIdQuery, IResult>
    {
        private readonly IStoreNikDbContext _dbContext;
        public GetRatingByOrderDetailIdQueryHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IResult> Handle(GetRatingByOrderDetailIdQuery request, CancellationToken cancellationToken)
        {
            var query = from r in _dbContext.Ratings
                        where r.OrderDetailId.Equals(request.OrderDetailId)
                        select r;
            if(request.IsOption)
            {
                var rating = await query.FirstOrDefaultAsync(cancellationToken);
                return FResult.Success(rating);
            }
            var count = await query.CountAsync(cancellationToken);
            return FResult.Success(count);
        }
    }
}
