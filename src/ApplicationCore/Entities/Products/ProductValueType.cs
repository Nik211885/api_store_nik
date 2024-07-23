using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities.Products
{
    public class ProductValueType : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string ProductNameTypeId { get; set; } = null!;
        public ProductNameType? ProductNameType { get; set; }
        /// <summary>
        /// Value of options name type
        /// </summary>
        /// 
        [Required]
        [MaxLength(50)]
        public string ValueType { get; set; } = null!;
        [Required]
        public bool OutOfSock { get; set; }
        [Required]
        /// <summary>
        /// Price in options
        /// </summary>
        public decimal Price { get; set; }
        public ProductValueType(string valueType, bool outOfSock, decimal price, string productNameTypeId)
        {
            ValueType = valueType;
            OutOfSock = outOfSock;
            Price = price;
            ProductNameTypeId = productNameTypeId;
        }
    }
}
