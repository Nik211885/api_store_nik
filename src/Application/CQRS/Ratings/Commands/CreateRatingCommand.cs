using Application.DTOs.Request;
using Application.Interface;
using FluentValidation;
using MediatR;

namespace Application.CQRS.Ratings.Commands
{
    public record CreateRatingCommand(string UserId, CreateRatingViewModel Rating) : IRequest<IResult>;
    public class CreateRatingCommandValidator : AbstractValidator<CreateRatingCommand>
    {
        public CreateRatingCommandValidator()
        {
            RuleFor(x => x.Rating).Must(s => s.Start >= 0 && s.Start <= 5)
                .WithMessage("Start must between 0-5 start");
        }
    }
}
