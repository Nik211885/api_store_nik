using Application.DTOs.Reponse;
using MediatR;

namespace Application.CQRS.Promotions.Queries
{
    public record GetListPromotionManagerByUserQuery(string UserId) : IRequest<IEnumerable<PromotionDiscountReponse>>;
}
