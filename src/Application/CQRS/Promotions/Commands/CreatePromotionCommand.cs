using Application.DTOs.Request;
using Application.Interface;
using FluentValidation;
using MediatR;

namespace Application.CQRS.Promotions.Commands
{
    public record CreatePromotionCommand(
        string UserId,
        PromotionViewModel Promotion
        ) : IRequest<IResult>;
    public class CreatePromotionCommandValidator : PromotionCommandValidator<CreatePromotionCommand> { };
    public class PromotionCommandValidator<T> : AbstractValidator<T> where T : CreatePromotionCommand
    {
        public PromotionCommandValidator()
        {
            RuleFor(x => x.Promotion.Name).NotNull().NotEmpty()
                    .WithMessage("Name promotion is not null");
            RuleFor(x => x.Promotion.Promotion).Must(p => p > 0 && p <= 100)
                .WithMessage("Promotion must between 0 - 100");
            RuleFor(x => x.Promotion).Must(x => x.EndDate > x.StartDay)
                .WithMessage("Promotion end date must bigger start day");
            RuleFor(x => x.Promotion.StartDay).Must(x => x >= DateTime.UtcNow)
                .WithMessage("Start Promotion Discount just apply start to day");
        }
    }
}
