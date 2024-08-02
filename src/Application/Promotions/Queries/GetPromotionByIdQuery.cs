using ApplicationCore.Entities.Products;
using MediatR;

namespace Application.Promotions.Queries
{
    public record GetPromotionByIdQuery(string Id) : IRequest<PromotionDiscount>;
}
