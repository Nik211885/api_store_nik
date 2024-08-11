using Application.DTOs.Reponse;
using MediatR;

namespace Application.CQRS.Products.Queries
{
    public record GetAllInformationForProductQuery(string ProductId) : IRequest<ProductDTO?>;
}
