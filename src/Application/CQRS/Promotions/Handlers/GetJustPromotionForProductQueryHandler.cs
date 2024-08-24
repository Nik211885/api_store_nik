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
            var promotionQuery = from p in _dbContext.ProductPromotionDiscounts
                                 where p.ProductId.Equals(request.ProductId)
                                 join po in _dbContext.PromotionDiscounts on p.PromotionDiscountId equals po.Id
                                 where po.EndDate >= DateTime.UtcNow && po.StartDay <= DateTime.UtcNow
                                 select po.Promotion;
            return await promotionQuery.ToListAsync(cancellationToken);
        }
    }
}
