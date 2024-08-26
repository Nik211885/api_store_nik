using Application.Common;
using Application.Common.Mappings;
using Application.CQRS.Promotions.Queries;
using Application.DTOs.Reponse;
using Application.Interface;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Promotions.Handlers
{
    public class GetListPromotionQueryHandler
        : IRequestHandler<GetListPromotionQuery, IEnumerable<PromotionDiscountReponse>>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetListPromotionQueryHandler(IStoreNikDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<PromotionDiscountReponse>> Handle(GetListPromotionQuery request, CancellationToken cancellationToken)
        {
            var query = from promotion in _dbContext.PromotionDiscounts
                        where promotion.EndDate >= DateTime.UtcNow
                        select promotion;
            var promotions = await query.ProjectTo<PromotionDiscountReponse>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return promotions;

        }
    }
}
