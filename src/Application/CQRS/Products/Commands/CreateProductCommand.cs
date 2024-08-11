using Application.Common.ResultTypes;
using Application.DTOs;
using FluentValidation;
using MediatR;

namespace Application.CQRS.Products.Commands
{
    public record CreateProductCommand(
        string UserId,
        string NameProduct,
        string Description,
        string ImageProduct,
        int Quantity,
        decimal Price,
        string? KeySearch,
        IEnumerable<ProductNameTypeViewModel> NameTypes
        ) : ProductUpdateViewModel(NameProduct, Description, ImageProduct,
            Quantity, Price, KeySearch, NameTypes), IRequest<Result>;

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x).NotNull().NotEmpty()
                 .WithMessage("Request is not null");
            RuleFor(x => x.NameProduct).NotEmpty()
                .WithMessage("Name product don't empty");
            RuleFor(x => x.Description).NotEmpty()
                .WithMessage("Descriptions don't empty");
            RuleFor(x => x.ImageProduct).NotEmpty()
                .WithMessage("Image product don't empty");
            RuleFor(x => x.Price).Must(p => p > 0)
                .WithMessage("Price must bigger 0");
            RuleFor(x => x.Quantity).Must(q => q > 0)
                .WithMessage("Quantity must bigger 0");
        }
    }
}
