using ApplicationCore.Entities.Products;
using MediatR;

namespace Application.CQRS.Promotions.Queries
{
    public record GetPromotionByIdQuery(string Id) : IRequest<PromotionDiscount>;
}
