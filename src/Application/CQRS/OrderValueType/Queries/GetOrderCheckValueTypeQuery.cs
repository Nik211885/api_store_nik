using MediatR;

namespace Application.CQRS.OrderValueType.Queries
{
    public record GetOrderCheckValueTypeQuery(string OrderId, string ValueTypeId)
        : IRequest<bool>;
}
