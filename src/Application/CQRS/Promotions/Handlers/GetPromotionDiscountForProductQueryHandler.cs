using Application.CQRS.Promotions.Queries;
using Application.DTOs.Reponse;
using Application.Interface;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Promotions.Handlers
{
    public class GetPromotionDiscountForProductQueryHandler :
        IRequestHandler<GetPromotionDiscountForProductQuery, IEnumerable<PromotionDiscountReponse>?>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetPromotionDiscountForProductQueryHandler(IStoreNikDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<PromotionDiscountReponse>?> Handle(GetPromotionDiscountForProductQuery request, CancellationToken cancellationToken)
        {
            var promotionQuery = from promotion in _dbContext.PromotionDiscounts
                                 where promotion.EndDate >= DateTime.UtcNow && promotion.StartDay <= DateTime.UtcNow
                                 join promotionForProduct in _dbContext.ProductPromotionDiscounts on promotion.Id equals promotionForProduct.PromotionDiscountId into p
                                 from po in p.DefaultIfEmpty()
                                 where promotion.ApplyAll
                                        || po.ProductId.Equals(request.ProductId)
                                 select promotion;
            var promotions = await promotionQuery.ProjectTo<PromotionDiscountReponse>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
            return promotions;
        }
    }
}
