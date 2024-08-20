namespace Application.DTOs.Request
{
    public record ProductDetailViewModel(
        string NameProduct,
        string Description,
        string ImageProduct,
        int Quantity,
        decimal Price,
        string? KeySearch,
        IEnumerable<ProductNameTypeViewModel>? NameTypes,
        IEnumerable<ProductDescriptionViewModel>? ProductDescription
        );
}
