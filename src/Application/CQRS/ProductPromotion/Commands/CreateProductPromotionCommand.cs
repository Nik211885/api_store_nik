using Application.Common.ResultTypes;
using MediatR;

namespace Application.CQRS.ProductPromotion.Commands
{
    public record CreateProductPromotionCommand(string ProductId, string PromotionDiscountId) : IRequest<Result>;
}
