using Application.Common.ResultTypes;
using FluentValidation;
using MediatR;

namespace Application.CQRS.OrderDetails.Command
{
    //User send server
    public record CreateOrderDetailCommand(
        string UserId,
        string ProductId,
        int Quantity,
        IEnumerable<string> ProductValueTypeIds
        ) : IRequest<Result>;
    
    public class CreateOrderDetailCommandValidator : AbstractValidator<CreateOrderDetailCommand>
    {
        public CreateOrderDetailCommandValidator()
        {
            RuleFor(x => x.Quantity).Must(x => x > 0)
                .WithMessage("Quantity must bigger 0");
        }
    }
}
