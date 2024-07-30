using ApplicationCore.Entities.Products;
using MediatR;

namespace Application.Promotions.Commands
{
    public record DeletePromotionCommand(PromotionDiscount Promotion) : IRequest;
}
