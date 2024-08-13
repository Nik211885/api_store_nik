using Application.Common.ResultTypes;
using Application.CQRS.ProductPromotion.Commands;
using Application.Interface;
using Application.Mappings;
using ApplicationCore.Entities.Products;
using MediatR;

namespace Application.CQRS.ProductPromotion.Handlers
{
    public class CreateProductPromotionCommandHandler : IRequestHandler<CreateProductPromotionCommand, IResult>
    {
        private readonly IStoreNikDbContext _dbContext;
        public CreateProductPromotionCommandHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IResult> Handle(CreateProductPromotionCommand request, CancellationToken cancellationToken)
        {
            var productPromotion = Mapping<CreateProductPromotionCommand, ProductPromotionDiscount>
                .CreateMap().Map<ProductPromotionDiscount>(request);
            _dbContext.ProductPromotionDiscounts.Add(productPromotion);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return FResult.Success();
        }
    }
}
