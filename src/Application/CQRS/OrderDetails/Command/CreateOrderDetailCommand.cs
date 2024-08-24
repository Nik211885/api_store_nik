using Application.DTOs.Request;
using Application.Interface;
using FluentValidation;
using MediatR;


namespace Application.CQRS.OrderDetails.Command
{
    //User send server
    public record CreateOrderDetailCommand(
        string UserId,
        CreateOrderViewModel Order
        ) : IRequest<IResult>;
    
    public class CreateOrderDetailCommandValidator : AbstractValidator<CreateOrderDetailCommand>
    {
        public CreateOrderDetailCommandValidator()
        {
            RuleFor(x => x.Order.Quantity).Must(x => x > 0)
                .WithMessage("Quantity must bigger 0");
        }
    }
}
