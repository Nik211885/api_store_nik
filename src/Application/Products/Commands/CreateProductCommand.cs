using Application.Common.ResultTypes;
using Application.DTOs;
using MediatR;

namespace Application.Products.Commands
{
    public record CreateProductCommand(
        string UserId,
        string NameProduct,
        string Description,
        string ImageProduct,
        int Quantity,
        decimal Price,
        string? KeySearch,
        IEnumerable<ProductNameTypeViewModel> NameTypes
        ) : ProductUpdateViewModel(NameProduct, Description, ImageProduct,
            Quantity, Price, KeySearch, NameTypes), IRequest<Result>;
}
