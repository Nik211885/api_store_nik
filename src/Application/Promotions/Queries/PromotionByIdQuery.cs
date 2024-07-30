using ApplicationCore.Entities.Products;
using MediatR;

namespace Application.Promotions.Queries
{
    public record PromotionByIdQuery(string Id) : IRequest<PromotionDiscount>;
}
