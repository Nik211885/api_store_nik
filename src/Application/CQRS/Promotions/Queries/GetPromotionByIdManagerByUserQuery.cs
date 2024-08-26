using ApplicationCore.Entities.Products;
using MediatR;

namespace Application.CQRS.Promotions.Queries
{
    public record GetPromotionByIdManagerByUserQuery(string PromotionId, string UserId, bool IsDate = false) : IRequest<PromotionDiscount>;
}
