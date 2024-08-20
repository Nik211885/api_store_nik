using Application.DTOs.Reponse;
using MediatR;

namespace Application.CQRS.Products.Queries
{
    public record GetProductValueTypeByNameTypeQuery(string NameTypeId) : IRequest<IEnumerable<ProductValueTypeReponse>>;
}
