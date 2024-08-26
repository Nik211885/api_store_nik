using Application.Interface;
using MediatR;

namespace Application.CQRS.ProductPromotion.Commands
{
    public record CreateProductPromotionCommand(string UserId, string ProductId, string PromotionDiscountId) : IRequest<IResult>;
}
