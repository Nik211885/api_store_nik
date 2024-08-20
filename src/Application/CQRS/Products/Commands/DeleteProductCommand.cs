using Application.Interface;
using FluentValidation;
using MediatR;

namespace Application.CQRS.Products.Commands
{
    public record DeleteProductCommand(string Id, string UserId) : IRequest<IResult>;
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x).NotNull()
                .WithMessage("Don't have product for delete");
        }
    }
}
