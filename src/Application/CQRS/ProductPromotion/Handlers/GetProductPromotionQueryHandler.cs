using Application.CQRS.ProductPromotion.Queries;
using Application.CQRS.Promotions.Queries;
using Application.Interface;
using ApplicationCore.Entities.Products;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ProductPromotion.Handlers
{
    internal class GetProductPromotionQueryHandler :
        IRequestHandler<GetProductPromotionQuery, ProductPromotionDiscount?>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly ISender _sender;
        public GetProductPromotionQueryHandler(IStoreNikDbContext dbContext, ISender sender)
        {
            _sender = sender;
            _dbContext = dbContext;
        }
        public async Task<ProductPromotionDiscount?> Handle(GetProductPromotionQuery request, CancellationToken cancellationToken)
        {
            //Check promotion has user manager
            await _sender.Send(new GetPromotionByIdManagerByUserQuery(request.PromotionId, request.UserId, true),cancellationToken);
            var productPromotionQuery = from pp in _dbContext.ProductPromotionDiscounts
                                        where pp.ProductId.Equals(request.ProductId)
                                                 && pp.PromotionDiscountId.Equals(request.PromotionId)
                                        select pp;
            var productPromotion = await productPromotionQuery.FirstOrDefaultAsync(cancellationToken);
            return productPromotion;
        }
    }
}
