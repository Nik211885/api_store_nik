using Application.Common.ResultTypes;
using MediatR;

namespace Application.Promotions.Commands
{
    public record UpdatePutPromotionCommand(string Id, string Name, string? Description, decimal Promotion, DateTime EndDate) : IRequest<Result>;
}
