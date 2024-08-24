namespace Application.DTOs.Request
{
    public record ProductDetailViewModel(
        string NameProduct,
        string Description,
        string ImageProduct,
        decimal Price,
        string? KeySearch,
        int Quantity,
        IEnumerable<ProductNameTypeViewModel>? NameTypes,
        IEnumerable<ProductDescriptionViewModel>? ProductDescription
        );
}
