using ApplicationCore.Entities.Order;
using MediatR;

namespace Application.CQRS.OrderDetails.Queries
{
    public record GetOrderDetailByCartIdQuery(string CartId) : IRequest<IEnumerable<OrderDetail>>;
}
