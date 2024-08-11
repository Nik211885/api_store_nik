using Application.Common.ResultTypes;
using MediatR;

namespace Application.CQRS.ProductPromotion.Commands
{
    public record DeleteProductPromotionCommand(string ProductId, string PromotionId) : IRequest<Result>;
}
