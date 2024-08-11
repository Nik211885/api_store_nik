using ApplicationCore.Entities.Attributes;
using ApplicationCore.Entities.Products;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities.Order
{
    public class OrderDetailProductValueType
    {
        [Required]
        [KeyGuidLength]
        public string OrderDetailId { get; private set; } = null!;
        public OrderDetail? OrderDetail { get; private set; }
        [Required]
        [KeyGuidLength]
        public string ProductValueTypeId { get; set; } = null!;
        public ProductValueType? ProductValueType { get; set; }
        public OrderDetailProductValueType(string orderDetailId, string productValueTypeId)
        {
            OrderDetailId = orderDetailId;
            ProductValueTypeId = productValueTypeId;
        }
    }
}
