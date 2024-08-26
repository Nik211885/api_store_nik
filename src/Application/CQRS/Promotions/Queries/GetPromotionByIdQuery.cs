using Application.DTOs.Reponse;
using MediatR;

namespace Application.CQRS.Promotions.Queries
{
    public record GetPromotionByIdQuery(string PromotionId) : IRequest<PromotionDiscountReponse>;
}
