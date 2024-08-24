using MediatR;

namespace Application.CQRS.Promotions.Queries
{
    public record GetJustPromotionForProductQuery(string ProductId)
        : IRequest<IEnumerable<decimal>>;
}
