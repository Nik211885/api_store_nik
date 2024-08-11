using ApplicationCore.Entities.Products;
using MediatR;

namespace Application.CQRS.ProductPromotion.Queries
{
    internal record GetProductPromotionQuery(string ProductId, string PromotionId) : IRequest<ProductPromotionDiscount?>;
}
