using ApplicationCore.Entities.Order;
using MediatR;

namespace Application.CQRS.OrderDetails.Queries
{
    internal record GetOrderDetailNotCheckOutOffUserQuery(string UserId, string OrderId)
        : IRequest<OrderDetail?>;
}
