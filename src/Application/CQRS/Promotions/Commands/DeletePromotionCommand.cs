using Application.Interface;
using MediatR;

namespace Application.CQRS.Promotions.Commands
{
    public record DeletePromotionCommand(string UserId, string PromotionId) : IRequest<IResult>;
}
