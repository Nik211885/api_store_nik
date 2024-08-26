using Application.CQRS.Promotions.Queries;
using Application.DTOs.Reponse;
using Application.Interface;
using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Promotions.Handlers
{
    public class GetPromotionByIdQueryHandler
        : IRequestHandler<GetPromotionByIdQuery, PromotionDiscountReponse>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetPromotionByIdQueryHandler(IStoreNikDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<PromotionDiscountReponse> Handle(GetPromotionByIdQuery request, CancellationToken cancellationToken)
        {
            var query = from p in _dbContext.PromotionDiscounts
                        where p.Id.Equals(request.PromotionId)
                        select p;
            var promotion = await query.ProjectTo<PromotionDiscountReponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken) ?? throw new ArgumentException("Don't find promotion");
            Guard.Against.Expression(x => x < DateTime.UtcNow, promotion.EndDate, nameof(promotion), "Promotion has exprise");
            return promotion;
        }
    }
}
