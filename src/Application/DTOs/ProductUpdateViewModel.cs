namespace Application.DTOs
{
    public record ProductUpdateViewModel(
        string NameProduct,
        string Description,
        string ImageProduct,
        int Quantity,
        decimal Price,
        string? KeySearch,
        IEnumerable<ProductNameTypeViewModel> NameTypes
        );
}
