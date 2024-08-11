using ApplicationCore.Entities.Attributes;
using ApplicationCore.Interface;
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
        /// <summary>
        ///     User have one cart not yet check out if user check out success turn
        ///     on event cart and off cart and create new cart empty
        /// </summary>
        public bool IsCheckOut { get; set; }
        public Cart(string userId)
        {
            UserId = userId;
            IsCheckOut = false;
        }
    }
}
