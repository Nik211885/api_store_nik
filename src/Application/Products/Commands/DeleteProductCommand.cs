using Application.Common.ResultTypes;
using MediatR;

namespace Application.Products.Commands
{
    public record DeleteProductCommand(string Id) : IRequest<Result>;
}
