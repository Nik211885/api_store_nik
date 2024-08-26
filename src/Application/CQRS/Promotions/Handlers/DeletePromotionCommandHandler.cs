using Application.Common.ResultTypes;
using Application.CQRS.Promotions.Commands;
using Application.CQRS.Promotions.Queries;
using Application.Interface;
using ApplicationCore.Entities.Products;
using ApplicationCore.Exceptions;
using ApplicationCore.ValueObject;
using MediatR;

namespace Application.CQRS.Promotions.Handlers
{
    public class DeletePromotionCommandHandler : IRequestHandler<DeletePromotionCommand, IResult>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly ISender _sender;
        public DeletePromotionCommandHandler(IStoreNikDbContext dbContext, ISender sender)
        {
            _dbContext = dbContext;
            _sender = sender;
        }
        public async Task<IResult> Handle(DeletePromotionCommand request, CancellationToken cancellationToken)
        {
            var promotion = await _sender.Send(new GetPromotionByIdManagerByUserQuery(request.PromotionId,request.UserId),cancellationToken);
            _dbContext.PromotionDiscounts.Remove(promotion);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return FResult.Success();
        }
    }
}
