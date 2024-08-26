using Application.CQRS.Promotions.Queries;
using Application.DTOs.Reponse;
using Application.Interface;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Promotions.Handlers
{
    public class GetListPromotionManagerByUserQueryHandler
        : IRequestHandler<GetListPromotionManagerByUserQuery, IEnumerable<PromotionDiscountReponse>>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetListPromotionManagerByUserQueryHandler(IStoreNikDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<PromotionDiscountReponse>> Handle(GetListPromotionManagerByUserQuery request, CancellationToken cancellationToken)
        {
            var query = from promotion in _dbContext.PromotionDiscounts
                        where promotion.UserId.Equals(request.UserId)
                        select promotion;
            var promotions = await query.ProjectTo<PromotionDiscountReponse>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
            return promotions;
        }
    }
}
