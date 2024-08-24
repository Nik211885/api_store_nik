using Application.DTOs.Request;
using Application.Interface;
using FluentValidation;
using MediatR;

namespace Application.CQRS.OrderDetails.Command
{
    public record UpdateOrderDetailCommand(string UserId, UpdateOrderViewModel Order)
        : IRequest<IResult>;
    public class UpdateOrderDetailCommandValidator :
        AbstractValidator<UpdateOrderDetailCommand>
    {
        public UpdateOrderDetailCommandValidator()
        {
            RuleFor(x => x.Order.Quantity).Must(x =>
            {
                if (x is null)
                {
                    return true;
                }
                if(x > 0)
                {
                    return true;
                }
                return false;
            }).WithMessage("Price for product value must bigger 0");
        }
    }
}
