using Application.Common.ResultTypes;
using Application.CQRS.Promotions.Commands;
using Application.CQRS.Promotions.Queries;
using Application.Interface;
using MediatR;

namespace Application.CQRS.Promotions.Handlers
{
    public class UpdatePatchPromotionCommandHandler
        : IRequestHandler<UpdatePatchPromotionCommand, IResult>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly ISender _sender;
        public UpdatePatchPromotionCommandHandler(IStoreNikDbContext dbContext, ISender sender)
        {
            _dbContext = dbContext;
            _sender = sender;
        }
        public async Task<IResult> Handle(UpdatePatchPromotionCommand request, CancellationToken cancellationToken)
        {
            var promotion = await _sender.Send(new GetPromotionByIdManagerByUserQuery(request.PromotionId, request.UserId), cancellationToken);
            //Don't support validator you can use model Valid
            request.PatchDoc.ApplyTo(promotion);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return FResult.Success();
        }
    }
}
