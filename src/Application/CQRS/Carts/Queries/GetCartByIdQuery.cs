using ApplicationCore.Entities.Order;
using MediatR;

namespace Application.CQRS.Carts.Queries
{
    public record GetCartByIdQuery(string Id) : IRequest<Cart?>;
}
