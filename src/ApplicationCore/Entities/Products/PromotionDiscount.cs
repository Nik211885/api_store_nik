using ApplicationCore.Entities.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities.Products
{
    public class PromotionDiscount : BaseEntity
    {
        /// <summary>
        /// User release coupons
        /// </summary>
        [Required]
        [KeyGuidLength]
        public string UserId { get; private set; } = null!;
        /// <summary>
        /// Defined name discount program
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = null!;
        /// <summary>
        /// Defined description discount program
        /// </summary>
        [MaxLength(120)]
        public string? Description { get; set; }
        /// <summary>
        /// Promotion must from (0-100]%
        /// </summary>
        [Required]
        [MinValue(0)]
        [MaxValue(100)]
        public decimal Promotion { get; set; }
        /// <summary>
        /// Start day begin discount program
        /// </summary>
        [Required]
        public DateTime StartDay { get; set; }
        /// <summary>
        /// day end discount program
        /// </summary>
        [Required]
        public DateTime EndDate { get; set; }
        public ICollection<ProductPromotionDiscount>? ProductPromotionDiscounts { get; set; }
        public bool ApplyAll { get; set; }
        public PromotionDiscount(string userId)
        {
            UserId = userId;
        }
    }
}
