using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities.Products
{
    public class ProductValueType : BaseEntity
    {
        [Required]
        [KeyGuidLength]
        public string ProductNameTypeId { get; private set; } = null!;
        public ProductNameType? ProductNameType { get; set; }
        /// <summary>
        /// Value of options name type
        /// </summary>
        /// 
        [Required]
        [MaxLength(50)]
        public string ValueType { get; set; } = null!;
        [Required]
        public int Quantity { get; set; }
        [Required]
        /// <summary>
        /// Price in options
        /// </summary>
        public decimal Price { get; set; }
        public ProductValueType(string valueType, int quantity, decimal price, string productNameTypeId)
        {
            ValueType = valueType;
            Quantity = quantity;
            Price = price;
            ProductNameTypeId = productNameTypeId;
        }
    }
}
