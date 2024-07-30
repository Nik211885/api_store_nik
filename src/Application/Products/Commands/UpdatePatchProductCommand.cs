using Application.Common.ResultTypes;
using ApplicationCore.Entities.Products;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.Products.Commands
{
    public record UpdatePatchProductCommand(string Id, JsonPatchDocument<Product> PatchDoc) : IRequest<Result>;
}
