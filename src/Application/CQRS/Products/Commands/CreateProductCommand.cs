using Application.DTOs.Request;
using Application.Interface;
using FluentValidation;
using MediatR;

namespace Application.CQRS.Products.Commands
{
    public record CreateProductCommand(
        string UserId,
        ProductDetailViewModel Product
        ) : IRequest<IResult>;
    public class CreateProductValidator : ProductDetailValidator<CreateProductCommand>
    {

    }
    public class ProductDetailValidator<T> : AbstractValidator<T> where T : CreateProductCommand
    {
        public ProductDetailValidator()
        {
            RuleFor(x => x).NotNull().NotEmpty()
                      .WithMessage("Request is not null");
            RuleFor(x => x.Product.NameTypes).NotEmpty()
                .WithMessage("Name product don't empty");
            RuleFor(x => x.Product.Description).NotEmpty()
                .WithMessage("Descriptions don't empty");
            RuleFor(x => x.Product.ImageProduct).NotEmpty()
                .WithMessage("Image product don't empty");
            RuleFor(x => x.Product.Price).Must(p => p >= 0)
                .WithMessage("Price must bigger 0");
            RuleFor(x => x.Product.Quantity).Must(x => x >= 0)
                .WithMessage("Quantity must bigger 0");
            RuleForEach(x => x.Product.NameTypes).Must(x =>
            {
                if (x is null)
                {
                    return true;
                }
                var count1 = x.ValeTypes.Count();
                var count = x.ValeTypes.Where(x => x.Price >= 0).Count();
                if (count == count1)
                {
                    return true;
                }
                return false;
            }).WithMessage("Price for product value must bigger 0");
        }
    }
}
