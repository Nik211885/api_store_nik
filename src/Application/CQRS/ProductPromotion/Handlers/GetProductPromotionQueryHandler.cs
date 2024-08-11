using Application.CQRS.ProductPromotion.Queries;
using Application.Interface;
using ApplicationCore.Entities.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ProductPromotion.Handlers
{
    internal class GetProductPromotionQueryHandler :
        IRequestHandler<GetProductPromotionQuery, ProductPromotionDiscount?>
    {
        private readonly IStoreNikDbContext _dbContext;
        public GetProductPromotionQueryHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ProductPromotionDiscount?> Handle(GetProductPromotionQuery request, CancellationToken cancellationToken)
        {
            var productPromotionQuery = from pp in _dbContext.ProductPromotionDiscounts
                                        where pp.ProductId.Equals(request.ProductId)
                                                 && pp.PromotionDiscountId.Equals(request.PromotionId)
                                        select pp;
            var productPromotion = await productPromotionQuery.FirstOrDefaultAsync(cancellationToken);
            return productPromotion;
        }
    }
}
