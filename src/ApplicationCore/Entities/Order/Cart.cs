using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities.Order
{
    public class Cart : BaseEntity
    {
        [Required]
        [KeyGuidLength]
        public string UserId { get; private set; } = null!;
        public ICollection<OrderDetail>? OrderDetails { get; set; }
        public Cart(string userId)
        {
            UserId = userId;
        }
    }
}
