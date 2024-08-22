using ApplicationCore.Entities.Attributes;
using ApplicationCore.Entities.Order;
using ApplicationCore.Entities.Ratings;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities.Products
{
    public class Product : BaseEntity
    {
        /// <summary>
        /// Foreign key for table Application User
        /// </summary>
        [Required]
        public string UserId { get; private set; } = null!;
        [Required]
        [MaxLength(50)]
        public string NameProduct { get; set; } = null!;
        [Required]
        [MaxLength(120)]
        public string Description { get; set; } = null!;
        [Required]
        [MaxLength(70)]
        public string ImageProduct { get; set; } = null!;
        [MinValue(1)]
        [Required]
        public int Quantity { get; set; }
        [Required]
        [MinValue(0)]
        public decimal Price { get; set; }
        [MaxLength(50)]
        public string? KeySearch { get; set; }
        public ICollection<ProductPromotionDiscount>? ProductPromotionDiscounts { get; set; }
        public ICollection<ProductNameType>? ProductNameTypes { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
        public ICollection<ProductDescription>? ProductDescriptions { get; set; }
        public Product(string userId) : base()
        {
            UserId = userId;
        }
        public Product(string userId, string nameProduct, string description, string imageProduct,
            decimal price, string? keySearch, int quantity)
        {
            UserId = userId;
            NameProduct = nameProduct;
            Description = description;
            ImageProduct = imageProduct;
            Price = price;
            KeySearch = keySearch;
            Quantity = quantity;
        }

    }
}
