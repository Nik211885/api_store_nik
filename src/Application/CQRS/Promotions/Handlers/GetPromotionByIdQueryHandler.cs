using Application.CQRS.Promotions.Queries;
using Application.Interface;
using ApplicationCore.Entities.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Promotions.Handlers
{
    public class GetPromotionByIdQueryHandler : IRequestHandler<GetPromotionByIdQuery, PromotionDiscount?>
    {
        private readonly IStoreNikDbContext _dbContext;
        public GetPromotionByIdQueryHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<PromotionDiscount?> Handle(GetPromotionByIdQuery request, CancellationToken cancellationToken)
        {
            var promotion = from p in _dbContext.PromotionDiscounts
                            where p.Id.Equals(request.Id)
                            select p;
            return await promotion.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
