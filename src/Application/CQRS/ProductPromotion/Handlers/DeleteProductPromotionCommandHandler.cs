using Application.Common.ResultTypes;
using Application.CQRS.ProductPromotion.Commands;
using Application.CQRS.ProductPromotion.Queries;
using Application.Interface;
using MediatR;

namespace Application.CQRS.ProductPromotion.Handlers
{
    public class DeleteProductPromotionCommandHandler : IRequestHandler<DeleteProductPromotionCommand, IResult>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly ISender _sender;
        public DeleteProductPromotionCommandHandler(IStoreNikDbContext dbContext, ISender sender)
        {
            _dbContext = dbContext;
            _sender = sender;
        }
        public async Task<IResult> Handle(DeleteProductPromotionCommand request, CancellationToken cancellationToken)
        {
            var productPromotion = await _sender.Send(new GetProductPromotionQuery(request.ProductId, request.PromotionId));
            if (productPromotion is null)
            {
                return FResult.Failure("Don't have program promotion");
            }
            _dbContext.ProductPromotionDiscounts.Remove(productPromotion);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return FResult.Success();
        }
    }
}
