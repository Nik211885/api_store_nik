using Application.Common.ResultTypes;
using Application.CQRS.ProductPromotion.Commands;
using Application.CQRS.ProductPromotion.Queries;
using Application.CQRS.Products.Queries;
using Application.CQRS.Promotions.Queries;
using Application.Interface;
using ApplicationCore.Entities.Products;
using MediatR;

namespace Application.CQRS.ProductPromotion.Handlers
{
    public class CreateProductPromotionCommandHandler : IRequestHandler<CreateProductPromotionCommand, IResult>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly ISender _sender;
        public CreateProductPromotionCommandHandler(IStoreNikDbContext dbContext, ISender sender)
        {
            _sender = sender;
            _dbContext = dbContext;
        }
        public async Task<IResult> Handle(CreateProductPromotionCommand request, CancellationToken cancellationToken)
        {
            //Check promotion has exprise and 
            var promotion = await _sender.Send(new GetPromotionByIdManagerByUserQuery(request.PromotionDiscountId, request.UserId, true),cancellationToken);
            if (promotion.ApplyAll)
            {
                return FResult.Failure("This promotion apply all product");
            }
            //Check product has for user manager
            await _sender.Send(new IsProductForUserQuery(request.UserId, request.ProductId), cancellationToken);
            var productPromotion = await _sender.Send(new GetProductPromotionQuery(request.UserId, request.ProductId, request.PromotionDiscountId),cancellationToken);
            if (productPromotion is not null) throw new ArgumentException("Product has add promotion");
            productPromotion = new ProductPromotionDiscount(request.ProductId,request.PromotionDiscountId);
            _dbContext.ProductPromotionDiscounts.Add(productPromotion);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return FResult.Success();
        }
    }
}
