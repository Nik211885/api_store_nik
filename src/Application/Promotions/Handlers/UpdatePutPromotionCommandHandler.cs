using Application.Common.ResultTypes;
using Application.Interface;
using Application.Mappings;
using Application.Promotions.Commands;
using Application.Promotions.Queries;
using ApplicationCore.Entities.Products;
using MediatR;

namespace Application.Promotions.Handlers
{
    public class UpdatePutPromotionCommandHandler : IRequestHandler<UpdatePutPromotionCommand, Result>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly ISender _sender;
        public UpdatePutPromotionCommandHandler(IStoreNikDbContext dbContext, ISender sender)
        {

            _dbContext = dbContext;
            _sender = sender;
        }
        public async Task<Result> Handle(UpdatePutPromotionCommand request, CancellationToken cancellationToken)
        {
            var promotion = await _sender.Send(new GetPromotionByIdQuery(request.Id));
            if(promotion is null)
            {
                return FResult.Failure(FErrors.NotFound(request.Id));
            }
            Mapping<UpdatePutPromotionCommand, PromotionDiscount>.CreateMap().Map(request, promotion);
            _dbContext.PromotionDiscounts.Update(promotion);
            return FResult.Success();
        }
    }
}
