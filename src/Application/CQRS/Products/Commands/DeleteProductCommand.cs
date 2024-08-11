using Application.Common.ResultTypes;
using MediatR;

namespace Application.CQRS.Products.Commands
{
    public record DeleteProductCommand(string Id) : IRequest<Result>;
}
