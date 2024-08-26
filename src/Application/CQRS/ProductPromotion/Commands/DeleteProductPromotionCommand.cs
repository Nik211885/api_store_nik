using Application.Interface;
using MediatR;

namespace Application.CQRS.ProductPromotion.Commands
{
    public record DeleteProductPromotionCommand(string UserId, string ProductId, string PromotionId) : IRequest<IResult>;
}
