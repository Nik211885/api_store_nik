using Application.DTOs.Reponse;
using MediatR;

namespace Application.CQRS.Products.Queries
{
    public record GetProductDescriptionQuery(string ProductId) : IRequest<IEnumerable<ProductDescriptionReponse>?>;
}
