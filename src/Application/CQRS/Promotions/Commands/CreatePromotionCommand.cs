using Application.Common.ResultTypes;
using FluentValidation;
using MediatR;

namespace Application.CQRS.Promotions.Commands
{
    public record CreatePromotionCommand(
        string UserId,
        string Name,
        string? Description,
        decimal Promotion,
        DateTime EndDate
        ) : IRequest<Result>;
    public class CreatePromotionCommandValidator : AbstractValidator<CreatePromotionCommand>
    {
        public CreatePromotionCommandValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty()
                    .WithMessage("Name promotion is not null");
            RuleFor(x => x.Promotion).Must(p => p > 0 && p <= 100)
                .WithMessage("Promotion must between 0 - 100");
        }
    }
}
