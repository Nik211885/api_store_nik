using Application.Interface;
using FluentValidation;
using MediatR;

namespace Application.CQRS.Ratings.Commands
{
    public record CreateRatingCommand(string UserId,
        string ProductId,
        float Start,
        string? CommentRating) : IRequest<IResult>;
    public class CreateRatingCommandValidator : AbstractValidator<CreateRatingCommand>
    {
        public CreateRatingCommandValidator()
        {
            RuleFor(x => x.Start).Must(s => s >= 0 && s <= 5)
                .WithMessage("Start must between 0-5 start");
        }
    }
}
