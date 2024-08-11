using Application.Common.ResultTypes;
using MediatR;

namespace Application.CQRS.OrderDetails.Command
{
    public record DeleteOrderDetailCommand(string Id) : IRequest<Result>;
}
