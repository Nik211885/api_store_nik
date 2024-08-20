using Application.Common.ResultTypes;
using Application.CQRS.Promotions.Commands;
using Application.CQRS.Promotions.Queries;
using Application.Interface;
using ApplicationCore.Entities.Products;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Promotions.Handlers
{
    public class UpdatePromotionCommandHandler : IRequestHandler<UpdatePromotionCommand, IResult>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ISender _sender;
        public UpdatePromotionCommandHandler(IStoreNikDbContext dbContext, ISender sender, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _sender = sender;
        }
        public async Task<IResult> Handle(UpdatePromotionCommand request, CancellationToken cancellationToken)
        {
            var promotion = await _sender.Send(new GetPromotionByIdQuery(request.Id), cancellationToken);
            if (promotion is null)
            {
                return FResult.NotFound(request.Id, nameof(PromotionDiscount));
            }
            promotion = _mapper.Map<PromotionDiscount>(request);
            _dbContext.PromotionDiscounts.Update(promotion);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return FResult.Success();
        }
    }
}
