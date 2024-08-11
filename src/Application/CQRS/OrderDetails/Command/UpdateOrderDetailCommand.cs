using Application.Common.ResultTypes;
using FluentValidation;
using MediatR;

namespace Application.CQRS.OrderDetails.Command
{
    public record UpdateOrderDetailCommand(string UserId, string OrderId, IEnumerable<string> ProductValueTypeId, int Quantity)
        : IRequest<Result>;
    public class UpdateOrderDetailCommandValidator :
        AbstractValidator<UpdateOrderDetailCommand>
    {
        public UpdateOrderDetailCommandValidator()
        {
            RuleFor(x => x.Quantity).Must(x => x > 0)
                .WithMessage("Quantity must bigger 0");
        }
    }
}
