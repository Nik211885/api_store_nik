using Application.Common;
using Application.Common.Mappings;
using Application.CQRS.Carts.Queries;
using Application.CQRS.Ratings.Queries;
using Application.DTOs.Reponse;
using Application.Interface;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Ratings.Handlers
{
    public class GetRatingWithPaginationByUseQueryHandler
        : IRequestHandler<GetRatingWithPaginationByUseQuery, PaginationEntity<RatingWithProductIdReponse>>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        public GetRatingWithPaginationByUseQueryHandler(IStoreNikDbContext dbContext, IMapper mapper, ISender sender)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _sender = sender;
        }
        public async Task<PaginationEntity<RatingWithProductIdReponse>> Handle(GetRatingWithPaginationByUseQuery request, CancellationToken cancellationToken)
        {
            var cartId = await _sender.Send(new GetCartIdByUserQuery(request.UserId,IsCheckOut: true),cancellationToken);
            var query = from od in _dbContext.OrderDetails
                        where cartId.Contains(od.CartId)
                        join r in _dbContext.Ratings on od.Id equals r.OrderDetailId
                        select new RatingWithProductIdReponse
                        {
                            ProductId = od.ProductId,
                            Id = r.Id,
                            Start = r.Start,
                            CommentRating = r.CommentRating,
                            DateRating = r.DateRating,
                        };

            var ratings = await PaginationEntity<RatingWithProductIdReponse>.CreatePaginationEntityAsync(query, request.PageNumber, request.PageSize);
            foreach(var item in ratings.Items)
            {
                await item.Join(_sender, request.UserId);
            }
            return ratings;
        }
    }
}
