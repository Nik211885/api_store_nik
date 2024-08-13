using Application.DTOs;
using Application.Interface;
using FluentValidation;
using MediatR;

namespace Application.CQRS.Products.Commands
{
    public record UpdatePutProductCommand(string Id, ProductUpdateViewModel Product) : IRequest<IResult>;
    public class UpdatePutProductCommandValidator : AbstractValidator<UpdatePutProductCommand>
    {
        public UpdatePutProductCommandValidator()
        {
            RuleFor(x => x).NotNull().NotEmpty()
                .WithMessage("Request is not null");
            RuleFor(x => x.Product.NameProduct).NotNull().NotEmpty()
                .WithMessage("Name product don't empty");
            RuleFor(x => x.Product.Description).NotNull().NotEmpty()
                .WithMessage("Descriptions don't empty");
            RuleFor(x => x.Product.ImageProduct).NotNull().NotEmpty()
                .WithMessage("Image product don't empty");
            RuleFor(x => x.Product.Price).Must(p => p > 0)
                .WithMessage("Price must bigger 0");
            RuleFor(x => x.Product.Quantity).Must(q => q > 0)
                .WithMessage("Quantity must bigger 0");
        }
    }
}
