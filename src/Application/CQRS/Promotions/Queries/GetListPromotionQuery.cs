using Application.DTOs.Reponse;
using MediatR;

namespace Application.CQRS.Promotions.Queries
{
    public record GetListPromotionQuery(int PageNumber = 1, int PageSize = 20) 
        : IRequest<IEnumerable<PromotionDiscountReponse>>;
}
