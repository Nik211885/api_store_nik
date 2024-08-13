using Application.Interface;
using MediatR;

namespace Application.CQRS.OrderDetails.Command
{
    public record DeleteOrderDetailCommand(string Id) : IRequest<IResult>;
}
