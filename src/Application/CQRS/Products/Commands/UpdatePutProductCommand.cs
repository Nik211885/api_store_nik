using Application.DTOs.Request;
using Application.Interface;
using MediatR;

namespace Application.CQRS.Products.Commands
{
    public record UpdatePutProductCommand(string UserId, string Id,
        ProductDetailViewModel Product)
            : CreateProductCommand(UserId,Product), IRequest<IResult>;
    public class UpdatePutProductValidator : ProductDetailValidator<UpdatePutProductCommand>
    {

    };
}
