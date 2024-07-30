using MediatR;

namespace Application.Promotions.Commands
{
    public record CreatePromotionCommand(
        string UserId,
        string Name,
        string? Description,
        decimal Promotion,
        DateTime EndDate
        ) : IRequest<string>;
}
