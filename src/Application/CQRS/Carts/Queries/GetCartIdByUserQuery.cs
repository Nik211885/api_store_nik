using MediatR;

namespace Application.CQRS.Carts.Queries
{
    internal record GetCartIdByUserQuery(string UserId) : IRequest<string>;
}
