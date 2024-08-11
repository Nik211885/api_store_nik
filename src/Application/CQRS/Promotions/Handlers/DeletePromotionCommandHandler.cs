using Application.Common.ResultTypes;
using Application.CQRS.Promotions.Commands;
using Application.CQRS.Promotions.Queries;
using Application.Interface;
using ApplicationCore.Entities.Products;
using MediatR;

namespace Application.CQRS.Promotions.Handlers
{
    public class DeletePromotionCommandHandler : IRequestHandler<DeletePromotionCommand, Result>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly ISender _sender;
        public DeletePromotionCommandHandler(IStoreNikDbContext dbContext, ISender sender)
        {
            _dbContext = dbContext;
            _sender = sender;
        }
        public async Task<Result> Handle(DeletePromotionCommand request, CancellationToken cancellationToken)
        {
            var promotion = await _sender.Send(new GetPromotionByIdQuery(request.Id),cancellationToken);
            if(promotion is null)
            {
                return FResult.NotFound(request.Id, nameof(PromotionDiscount));
            }
            _dbContext.PromotionDiscounts.Remove(promotion);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return FResult.Success();
        }
    }
}
