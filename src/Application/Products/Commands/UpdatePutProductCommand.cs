using Application.Common.ResultTypes;
using Application.DTOs;
using MediatR;

namespace Application.Products.Commands
{
    public record UpdatePutProductCommand(string Id, ProductUpdateViewModel Product) : IRequest<Result>;
}
