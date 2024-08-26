using Application.Common;
using Application.Common.Mappings;
using Application.CQRS.Products.Queries;
using Application.CQRS.Promotions.Handlers;
using Application.CQRS.Promotions.Queries;
using Application.DTOs.Reponse;
using Application.Interface;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.CQRS.Products.Handlers
{
    public class GetProductHasPromotionWithPaginationQueryHandler
        : IRequestHandler<GetProductHasPromotionWithPaginationQuery, PaginationEntity<ProductDashboardReponse>?>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        public GetProductHasPromotionWithPaginationQueryHandler(IStoreNikDbContext dbContext, ISender sender, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _sender = sender;
        }
        public async Task<PaginationEntity<ProductDashboardReponse>?> Handle(GetProductHasPromotionWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var promotion = await _sender.Send(new GetPromotionByIdQuery(request.PromotionId),cancellationToken);
            if (promotion.ApplyAll)
            {
                return null;
            }
            var query = from ppd in _dbContext.ProductPromotionDiscounts
                        where ppd.PromotionDiscountId.Equals(request.PromotionId)
                        join product in _dbContext.Products on ppd.ProductId equals product.Id
                        select product;
            var products = await query.ProjectTo<ProductDashboardReponse>(_mapper.ConfigurationProvider)
                            .PaginatedListAsync(request.PageNumber, request.PageSize);
            foreach(var item in products.Items)
            {
                await item.Join(_sender);
            }
            return products;
        }
    }
}
