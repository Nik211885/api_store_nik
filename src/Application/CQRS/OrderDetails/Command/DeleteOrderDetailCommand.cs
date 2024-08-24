using Application.Interface;
using MediatR;

namespace Application.CQRS.OrderDetails.Command
{
    public record DeleteOrderDetailCommand(string UserId, string OrderId) : IRequest<IResult>;
}
