using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities.Products
{
    public class ProductItem : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string NameProduct { get; set; } = null!;
        [Required]
        [MaxLength(120)]
        public string Description { get; set; } = null!;
        [Required]
        [MaxLength(70)]
        public string ImageDescription { get; set; } = null!;
        [Required]
        public bool OutOfSock { get; set; }
        [Required]
        public decimal Price { get; set; }
        [MaxLength(50)]
        public string? KeySearch { get; set; }
        public ICollection<ProductPromotionDiscount>? PromotionProducts { get; set; }
        public ICollection<ProductNameType>? ProductNameTypes { get; set; }
        public ProductItem(string nameProduct, string description, string imageProduct,
            decimal price, string? keySearch, bool outOfSock)
        {
            NameProduct = nameProduct;
            Description = description;
            ImageDescription = imageProduct;
            Price = price;
            KeySearch = keySearch;
            OutOfSock = outOfSock;
        }
    }
}
