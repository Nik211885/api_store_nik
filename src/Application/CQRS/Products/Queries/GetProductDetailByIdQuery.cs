using Application.DTOs.Reponse;
using MediatR;

namespace Application.CQRS.Products.Queries
{
    public record GetProductDetailByIdQuery(string Id, string? accessToken = null) : IRequest<ProductDetailReponse>;
}
