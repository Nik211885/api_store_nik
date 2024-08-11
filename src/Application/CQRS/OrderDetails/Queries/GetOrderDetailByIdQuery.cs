using ApplicationCore.Entities.Order;
using MediatR;

namespace Application.CQRS.OrderDetails.Queries
{
    internal record GetOrderDetailByIdQuery(string Id, bool IsCheckOut = false) : IRequest<OrderDetail?>;
}
