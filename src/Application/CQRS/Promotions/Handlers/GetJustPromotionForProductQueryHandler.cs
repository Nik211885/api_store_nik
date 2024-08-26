using Application.CQRS.Promotions.Queries;
using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Promotions.Handlers
{
    public class GetJustPromotionForProductQueryHandler
        : IRequestHandler<GetJustPromotionForProductQuery, IEnumerable<decimal>>
    {
        private readonly IStoreNikDbContext _dbContext;
        public GetJustPromotionForProductQueryHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<decimal>> Handle(GetJustPromotionForProductQuery request, CancellationToken cancellationToken)
        {
            var promotionQuery = from promotion in _dbContext.PromotionDiscounts
                                 where promotion.EndDate >= DateTime.UtcNow && promotion.StartDay <= DateTime.UtcNow
                                 join promotionForProduct in _dbContext.ProductPromotionDiscounts on promotion.Id equals promotionForProduct.PromotionDiscountId into p
                                 from po in p.DefaultIfEmpty()
                                 where promotion.ApplyAll
                                        || po.ProductId.Equals(request.ProductId)
                                 select promotion.Promotion;
            return await promotionQuery.ToListAsync(cancellationToken);
        }
    }
}
