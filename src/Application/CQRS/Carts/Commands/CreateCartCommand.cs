using Application.Common.ResultTypes;
using MediatR;

namespace Application.CQRS.Carts.Commands
{
    public record class CreateCartCommand(string UserId) : IRequest<Result>;
}
