using Application.DTOs.Reponse;
using MediatR;

namespace Application.CQRS.Promotions.Queries
{
    public record GetPromotionDiscountForProductQuery(string ProductId): IRequest<IEnumerable<PromotionDiscountReponse>>;
}
