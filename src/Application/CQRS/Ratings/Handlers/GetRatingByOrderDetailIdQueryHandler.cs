using Application.Common.ResultTypes;
using Application.CQRS.Ratings.Queries;
using Application.DTOs.Reponse;
using Application.Interface;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Ratings.Handlers
{
    public class GetRatingByOrderDetailIdQueryHandler
        : IRequestHandler<GetRatingByOrderDetailIdQuery, IResult>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly ISender _sender;
        public GetRatingByOrderDetailIdQueryHandler(IStoreNikDbContext dbContext, ISender sender)
        {
            _dbContext = dbContext;
            _sender = sender;
        }
        public async Task<IResult> Handle(GetRatingByOrderDetailIdQuery request, CancellationToken cancellationToken)
        {
            var query = from od in _dbContext.OrderDetails
                        where od.Id.Equals(request.OrderDetailId)
                        join r in _dbContext.Ratings on od.Id equals r.OrderDetailId
                        select new RatingWithProductIdReponse
                        {
                            ProductId = od.ProductId,
                            Id = r.Id,
                            Start = r.Start,
                            CommentRating = r.CommentRating,
                            DateRating = r.DateRating,
                        };
            //One to one relationship between rating and order detail table 
            if (request.IsOption)
            {
                var rating = await query.FirstOrDefaultAsync(cancellationToken);
                if (rating is not null) await rating.Join(_sender,request.UserId);
                return FResult.Success(rating);
            }
            var count = await query.CountAsync(cancellationToken);
            return FResult.Success(count);
        }
    }
}
