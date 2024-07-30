using Application.Interface;
using Application.Promotions.Queries;
using ApplicationCore.Entities.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Promotions.Handlers
{
    public class PromotionByIdQueryHandler : IRequestHandler<PromotionByIdQuery, PromotionDiscount?>
    {
        private readonly IStoreNikDbContext _dbContext;
        public PromotionByIdQueryHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<PromotionDiscount?> Handle(PromotionByIdQuery request, CancellationToken cancellationToken)
        {
            var promotion = from p in _dbContext.PromotionDiscounts
                            where p.Id.Equals(request.Id)
                            select p;
            return await promotion.FirstOrDefaultAsync();
        }
    }
}
