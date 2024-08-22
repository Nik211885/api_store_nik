using ApplicationCore.Entities.Attributes;
using ApplicationCore.Entities.Products;
using ApplicationCore.Entities.Ratings;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities.Order
{
    public class OrderDetail : BaseEntity
    {
        [Required]
        [KeyGuidLength]
        public string CartId { get; private set; } = null!;
        public Cart? Cart { get; set; }
        [MinValue(1)]
        public int Quantity { get; set; }
        [Required]
        [KeyGuidLength]
        public string ProductId { get; private set; } = null!;
        public Product? Product { get; set; }
        public Rating? Rating { get; set; }
        public ICollection<OrderDetailProductValueType>? OrderDetailProductValueTypes { get; set; }
        public OrderDetail(string cartId, string productId, int quantity = 1)
        {
            CartId = cartId;
            Quantity = quantity;
            ProductId = productId;
        }
    }
}
