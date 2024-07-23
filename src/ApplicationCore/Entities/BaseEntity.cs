using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    public abstract class BaseEntity 
    {
        /// <summary>
        /// Defined primary key in entity for table
        /// </summary>
        [Key]
        [Required]
        [MaxLength(50)]
        public string Id { get; set; } = null!;
        protected BaseEntity()
        {
            Id = Guid.NewGuid().ToString(); 
        }
    }
}
