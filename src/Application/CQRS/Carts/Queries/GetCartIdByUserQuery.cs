using MediatR;

namespace Application.CQRS.Carts.Queries
{
    internal record GetCartIdByUserQuery(string UserId, bool IsCheckOut = false) : IRequest<IEnumerable<string>>;
}
