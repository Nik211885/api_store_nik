using Application.Common.ResultTypes;
using MediatR;

namespace Application.CQRS.Promotions.Commands
{
    public record DeletePromotionCommand(string Id) : IRequest<Result>;
}
