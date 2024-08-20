using Application.DTOs.Reponse;
using MediatR;

namespace Application.CQRS.Products.Queries
{
    public record GetProductNameTypeQuery(string ProductId) : IRequest<IEnumerable<ProductNameTypeReponse>?>;
}
