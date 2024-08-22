using Application.Common;
using Application.Common.Mappings;
using Application.CQRS.Ratings.Queries;
using Application.DTOs.Reponse;
using Application.Interface;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.CQRS.Ratings.Handlers
{
    public class GetRatingForProductWithPaginationQueryHandler : IRequestHandler<GetRatingForProductWithPaginationQuery, PaginationEntity<RatingReponse>>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        public GetRatingForProductWithPaginationQueryHandler(IStoreNikDbContext dbContext, IMapper mapper, ISender sender)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _sender = sender;
        }
        public async Task<PaginationEntity<RatingReponse>> Handle(GetRatingForProductWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var queryRatings = from od in _dbContext.OrderDetails
                               where od.ProductId.Equals(request.ProductId)
                               join rating in _dbContext.Ratings on od.Id equals rating.OrderDetailId
                               select rating;
            var ratings = await queryRatings.ProjectTo<RatingReponse>(_mapper.ConfigurationProvider).PaginatedListAsync(request.PageNumber, request.PageSize);
            foreach (var r in ratings.Items)
            {
                await r.Join(_sender);
            }
            return ratings;
        }
    }
}
