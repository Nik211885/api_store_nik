using Application.Common.ResultTypes;
using Application.CQRS.Promotions.Commands;
using Application.CQRS.Promotions.Queries;
using Application.Interface;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Promotions.Handlers
{
    public class UpdatePutPromotionCommandHandler : IRequestHandler<UpdatePutPromotionCommand, IResult>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ISender _sender;
        public UpdatePutPromotionCommandHandler(IStoreNikDbContext dbContext, ISender sender, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _sender = sender;
        }
        public async Task<IResult> Handle(UpdatePutPromotionCommand request, CancellationToken cancellationToken)
        {
            var promotion = await _sender.Send(new GetPromotionByIdManagerByUserQuery(request.PromotionId,request.UserId), cancellationToken);
            promotion = _mapper.Map(request.Promotion,promotion);
            _dbContext.PromotionDiscounts.Update(promotion);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return FResult.Success();
        }
    }
}
