using Application.DTOs.Reponse;
using MediatR;

namespace Application.CQRS.Promotions.Queries
{
    public record GetPromotionForProductQuery(string ProductId): IRequest<IEnumerable<PromotionDiscountReponse>>;
}
