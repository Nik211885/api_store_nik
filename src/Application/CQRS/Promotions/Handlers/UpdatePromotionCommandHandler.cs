using Application.Common.ResultTypes;
using Application.CQRS.Promotions.Commands;
using Application.CQRS.Promotions.Queries;
using Application.Interface;
using Application.Mappings;
using ApplicationCore.Entities.Products;
using MediatR;

namespace Application.CQRS.Promotions.Handlers
{
    public class UpdatePromotionCommandHandler : IRequestHandler<UpdatePromotionCommand, Result>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly ISender _sender;
        public UpdatePromotionCommandHandler(IStoreNikDbContext dbContext, ISender sender)
        {

            _dbContext = dbContext;
            _sender = sender;
        }
        public async Task<Result> Handle(UpdatePromotionCommand request, CancellationToken cancellationToken)
        {
            var promotion = await _sender.Send(new GetPromotionByIdQuery(request.Id), cancellationToken);
            if (promotion is null)
            {
                return FResult.NotFound(request.Id, nameof(PromotionDiscount));
            }
            Mapping<UpdatePromotionCommand, PromotionDiscount>.CreateMap().Map(request, promotion);
            _dbContext.PromotionDiscounts.Update(promotion);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return FResult.Success();
        }
    }
}
