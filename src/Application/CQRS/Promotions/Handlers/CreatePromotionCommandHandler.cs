using Application.Common.ResultTypes;
using Application.CQRS.Promotions.Commands;
using Application.Interface;
using Application.Mappings;
using ApplicationCore.Entities.Products;
using MediatR;

namespace Application.CQRS.Promotions.Handlers
{
    public class CreatePromotionCommandHandler :
        IRequestHandler<CreatePromotionCommand, Result>
    {
        private readonly IStoreNikDbContext _dbContext;
        public CreatePromotionCommandHandler(IStoreNikDbContext dbContext, ISender sender)
        {
            _dbContext = dbContext;
        }
        public async Task<Result> Handle(CreatePromotionCommand request, CancellationToken cancellationToken)
        {
            var promotion = Mapping<CreatePromotionCommand, PromotionDiscount>.CreateMap().Map<PromotionDiscount>(request);
            _dbContext.PromotionDiscounts.Add(promotion);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return FResult.Success();
        }
    }
}
