using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities.Products
{
    public class PromotionDiscount : BaseEntity
    {
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
        public ICollection<ProductPromotionDiscount>? PromotionProduct { get; set; }
        public PromotionDiscount(string name, string? description,
            decimal promotion, DateTime startDay, DateTime endDate)
        {
            Name = name;
            Description = description;
            Promotion = promotion;
            StartDay = startDay;
            EndDate = endDate;
        }
    }
}
