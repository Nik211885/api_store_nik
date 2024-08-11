namespace Application.DTOs.Reponse
{
    public class OrderDetailDTO
    {
        public string Id { get; private set; } = null!;
        public string ProductId { get; private set; } = null!;
        public int Quantity { get; set; }
        public string ImageProduct { get; private set; } = null!;
        public string ProductName { get; private set; } = null!;
        public decimal Price { get; private set; }
        public ICollection<ProductValueTypeDTO>? ValueType { get; set; }
        public OrderDetailDTO(string id, string productId, int quantity, string imageProduct, string productName, decimal price)
        {
            Id = id;
            ProductId = productId;
            Quantity = quantity;
            ImageProduct = imageProduct;
            ProductName = productName;
            Price = price;
        }
    }
    public class ProductValueTypeDTO
    {
        public string ValueTypeId { get; set; } = null!;
        public string ValueType { get; private set; } = null!;
        public decimal Price { get; private set; }
        public ProductValueTypeDTO(string valueTypeId, string valueType, decimal price)
        {
            ValueType = valueType;
            ValueTypeId = valueTypeId;
            Price = price;
        }

    }
}
