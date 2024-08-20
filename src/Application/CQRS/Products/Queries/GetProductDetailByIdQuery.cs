using Application.DTOs.Reponse;
using MediatR;

namespace Application.CQRS.Products.Queries
{
    public record GetProductDetailByIdQuery(string Id) : IRequest<ProductDetailReponse?>;
}
