using Application.DTOs.Reponse;
using MediatR;

namespace Application.CQRS.OrderDetails.Queries
{
    public record GetOrderForUserHasNotCheckOutQuery(string UserId) 
        : IRequest<IEnumerable<OrderHasNotCheckOutReponse>?>;
}
