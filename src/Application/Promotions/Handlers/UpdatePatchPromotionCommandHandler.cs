using Application.Common.ResultType;
using Application.Common.ResultTypes;
using Application.Interface;
using Application.Promotions.Commands;
using Application.Promotions.Queries;
using MediatR;

namespace Application.Promotions.Handlers
{
    public class UpdatePatchPromotionCommandHandler : IRequestHandler<UpdatePatchPromotionCommand, Result>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly ISender _sender;

        public UpdatePatchPromotionCommandHandler(IStoreNikDbContext dbContext, ISender sender)
        {
            _dbContext = dbContext;
            _sender = sender;
        }
        public async Task<Result> Handle(UpdatePatchPromotionCommand request, CancellationToken cancellationToken)
        {
            if(request.PatchDoc is null)
            {
                return FResult.Failure(new ResultError("Error", "Doc patch is not null"));
            }
            var promotion = await _sender.Send(new GetPromotionByIdQuery(request.Id),cancellationToken);
            if(promotion is null)
            {
                return FResult.Failure(FErrors.NotFound(request.Id));
            }
            request.PatchDoc.ApplyTo(promotion);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return FResult.Success();
        }
    }
}
