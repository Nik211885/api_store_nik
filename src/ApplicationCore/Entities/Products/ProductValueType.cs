using ApplicationCore.Entities.Attributes;
using ApplicationCore.Entities.Order;
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
        /// <summary>
        /// Price in options
        /// </summary>
        /// 
        [MinValue(0)]
        public decimal Price { get; set; }
        //public string? ImageDescription { get; set; }
        //public string? Description { get; set; }
        public ICollection<OrderDetailProductValueType>? OrderDetailProductValueTypes { get; set; }
        public ProductValueType(string valueType, decimal price, string productNameTypeId)
        {
            ValueType = valueType;
            Price = price;
            ProductNameTypeId = productNameTypeId;
        }
    }
}
