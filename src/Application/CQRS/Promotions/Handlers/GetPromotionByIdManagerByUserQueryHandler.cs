using Application.Interface;
using ApplicationCore.Entities.Products;
using ApplicationCore.Exceptions;
using ApplicationCore.ValueObject;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Promotions.Handlers
{
    public class GetPromotionByIdManagerByUserQueryHandler 
        : IRequestHandler<Queries.GetPromotionByIdManagerByUserQuery, PromotionDiscount?>
    {
        private readonly IStoreNikDbContext _dbContext;
        public GetPromotionByIdManagerByUserQueryHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<PromotionDiscount?> Handle(Queries.GetPromotionByIdManagerByUserQuery request, CancellationToken cancellationToken)
        {
            var query = from p in _dbContext.PromotionDiscounts
                            where p.Id.Equals(request.PromotionId)
                            select p;
            var promotion = await query.FirstOrDefaultAsync(cancellationToken) ?? throw new ArgumentException("Don't find promotion");
            if (!promotion.UserId.Equals(request.UserId))
            {
                throw new ForbiddenException(VariableException.Forbidden);
            }
            if(request.IsDate)
            {
                Guard.Against.Expression(x => x < DateTime.UtcNow, promotion.EndDate, nameof(PromotionDiscount), "Promotion has exprise");
            }
            return promotion;
        }
    }
}
