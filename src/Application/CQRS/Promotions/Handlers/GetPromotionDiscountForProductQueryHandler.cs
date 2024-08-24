using Application.CQRS.Promotions.Queries;
using Application.DTOs.Reponse;
using Application.Interface;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Promotions.Handlers
{
    public class GetPromotionDiscountForProductQueryHandler :
        IRequestHandler<GetPromotionDiscountForProductQuery, IEnumerable<PromotionDiscountReponse>?>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetPromotionDiscountForProductQueryHandler(IStoreNikDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<PromotionDiscountReponse>?> Handle(GetPromotionDiscountForProductQuery request, CancellationToken cancellationToken)
        {
            var promotionQuery = from p in _dbContext.ProductPromotionDiscounts
                                 where p.ProductId.Equals(request.ProductId)
                                 join po in _dbContext.PromotionDiscounts on p.PromotionDiscountId equals po.Id
                                 where po.EndDate >= DateTime.UtcNow && po.StartDay <= DateTime.UtcNow
                                 select po;
            var promotion = await promotionQuery.ProjectTo<PromotionDiscountReponse>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
            return promotion;
        }
    }
}
