using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities.Products
{
    public class ProductDescription : BaseEntity
    {
        [MaxLength(50)]
        public string NameDescription { get; set; } = null!;
        [MaxLength(200)]
        public string ValueDescription { get; set; } = null!;
        public Product? Product { get; set; }
        public string ProductId { get; set; } = null!;
        public ProductDescription(string productId, string nameDescription, string valueDescription)
        {
            ProductId = productId;
            NameDescription = nameDescription;
            ValueDescription = valueDescription;
        }
    }
}
