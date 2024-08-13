using Application.Interface;
using FluentValidation;
using MediatR;

namespace Application.CQRS.Promotions.Commands
{
    public record UpdatePromotionCommand(string Id, string Name, string? Description, decimal Promotion, DateTime EndDate) : IRequest<IResult>;
    public class UpdatePromotionCommandValidator : AbstractValidator<UpdatePromotionCommand>
    {
        public UpdatePromotionCommandValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty()
                    .WithMessage("Name promotion is not null");
            RuleFor(x => x.Promotion).Must(p => p > 0 && p <= 100)
                .WithMessage("Promotion must between 0 - 100");
        }
    }
}
