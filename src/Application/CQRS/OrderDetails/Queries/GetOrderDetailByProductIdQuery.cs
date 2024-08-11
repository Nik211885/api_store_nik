using ApplicationCore.Entities.Order;
using MediatR;

namespace Application.CQRS.OrderDetails.Queries
{
    public record GetOrderDetailByProductIdQuery(string ProductId) : IRequest<IEnumerable<OrderDetail>>;
}
