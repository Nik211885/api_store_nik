namespace Application.DTOs.Request
{
    public record CreateOrderViewModel(
        string ProductId,
        int Quantity,
        IEnumerable<string>? ProductValueTypeIds);
}
