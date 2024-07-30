namespace Application.DTOs
{
    public record ProductNameTypeViewModel(string NameType,
        IEnumerable<ProductValueTypeViewModel> ValeTypes
        );
}
