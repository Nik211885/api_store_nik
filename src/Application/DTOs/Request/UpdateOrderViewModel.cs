namespace Application.DTOs.Request
{
    public record UpdateOrderViewModel(string OrderId,
                                       IEnumerable<string>? ProductValueTypeId,
                                       int? Quantity);
}
