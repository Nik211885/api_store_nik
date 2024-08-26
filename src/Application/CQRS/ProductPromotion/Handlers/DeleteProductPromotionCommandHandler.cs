using Application.Common.ResultTypes;
using Application.CQRS.ProductPromotion.Commands;
using Application.CQRS.ProductPromotion.Queries;
using Application.Interface;
using ApplicationCore.Entities.Products;
using Ardalis.GuardClauses;
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
            var productPromotion = await _sender.Send(new GetProductPromotionQuery(request.UserId, request.ProductId, request.PromotionId),cancellationToken);
            Guard.Against.Null(productPromotion,nameof(Product), "Product hasn't promotion");
            _dbContext.ProductPromotionDiscounts.Remove(productPromotion);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return FResult.Success();
        }
    }
}
