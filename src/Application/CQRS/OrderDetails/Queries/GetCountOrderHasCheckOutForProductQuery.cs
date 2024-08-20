using MediatR;

namespace Application.CQRS.OrderDetails.Queries
{
    internal record GetCountOrderHasCheckOutForProductQuery(string ProductId) : IRequest<int>;
}
