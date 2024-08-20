using FluentValidation;

namespace Application.DTOs
{
    public record ProductValueTypeViewModel(string ValueType, int Quantity, decimal Price);
    public class ProductValueTypeValidator 
        : AbstractValidator<ProductValueTypeViewModel>
    {
        public ProductValueTypeValidator()
        {
            RuleFor(x => x.Price).Must(x => x >= 0)
                .WithMessage("Price of product value type must bigger 0");
            RuleFor(x => x.Quantity).Must(x => x >= 0)
                .WithMessage("Quantity of product value type must bigger 0");
        }
    }
}
