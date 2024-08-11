using Application.Common.ResultTypes;
using ApplicationCore.Entities.Products;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.CQRS.Products.Commands
{
    public record UpdatePatchProductCommand(string Id, JsonPatchDocument<Product> PatchDoc) : IRequest<Result>;
    public class UpdatePatchProductCommandValidator : AbstractValidator<UpdatePatchProductCommand>
    {
        public UpdatePatchProductCommandValidator()
        {
            RuleFor(x => x).NotNull().NotEmpty()
                .WithMessage("Request is not null");
            RuleFor(x => x.PatchDoc).NotNull().NotEmpty()
                .WithMessage("Patch doc is not empty");
        }
    }
}
