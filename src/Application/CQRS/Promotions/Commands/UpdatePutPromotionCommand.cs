using Application.DTOs.Request;
using Application.Interface;
using MediatR;

namespace Application.CQRS.Promotions.Commands
{
    public record UpdatePutPromotionCommand(string UserId, 
        string PromotionId,
        PromotionViewModel Promotion
        ) : CreatePromotionCommand(UserId,Promotion), IRequest<IResult>;
    public class UpdatePromotionCommandValidator : PromotionCommandValidator<UpdatePutPromotionCommand> { }
}
