using Application.Interface;
using MediatR;

namespace Application.CQRS.Products.Commands
{
    public record DeleteProductCommand(string Id) : IRequest<IResult>;
}
