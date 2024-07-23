using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities.Products
{
    /// <summary>
    ///Convert Relationship many to many between promotion and product
    /// </summary>
    public class ProductPromotionDiscount
    {
        [Required]
        [MaxLength(50)]
        public string ProductId { get; set; } = null!;
        public ProductItem? Product { get; set; }
        [Required]
        [MaxLength(50)]
        public string PromotionDiscountId { get; set; } = null!;
        public PromotionDiscount? PromotionDiscount { get; set; }
        public ProductPromotionDiscount(string productId, string promotionDiscountId)
        {
            ProductId = productId;
            PromotionDiscountId = promotionDiscountId;
        }
    }
}
