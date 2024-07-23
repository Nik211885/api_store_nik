using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities.Products
{
    public class ProductNameType : BaseEntity
    {
        /// <summary>
        /// Defined name type options if product need
        /// </summary>
        [Required]
        [MaxLength(25)]
        public string NameType { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string ProductId { get; set; } = null!;
        public ProductItem? ProductItem { get; set; }
        public ICollection<ProductValueType>? ProductValueTypes { get; set; }
        public ProductNameType(string nameType, string productId)
        {
            ProductId = productId;
            NameType = nameType;
        }
    }
}
