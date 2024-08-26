using Application.Interface;
using ApplicationCore.Entities.Products;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.CQRS.Promotions.Commands
{
    public record UpdatePatchPromotionCommand(string UserId, string PromotionId, JsonPatchDocument<PromotionDiscount> PatchDoc) : IRequest<IResult>;
    public class UpdatePatchPromotionValidator : AbstractValidator<UpdatePatchPromotionCommand>
    {
        public UpdatePatchPromotionValidator()
        {
            RuleFor(x=>x.PatchDoc).NotEmpty();
        }
    }
}
