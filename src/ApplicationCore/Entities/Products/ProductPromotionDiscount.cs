using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities.Products
{
    /// <summary>
    ///Convert Relationship many to many between promotion and product
    /// </summary>
    public class ProductPromotionDiscount
    {
        [Required]
        [KeyGuidLength]
        public string ProductId { get; private set; } = null!;
        public Product? Product { get; set; }
        [Required]
        [KeyGuidLength]
        public string PromotionDiscountId { get; private set; } = null!;
        public PromotionDiscount? PromotionDiscount { get; set; }
        public ProductPromotionDiscount(string productId, string promotionDiscountId)
        {
            ProductId = productId;
            PromotionDiscountId = promotionDiscountId;
        }
    }
}
