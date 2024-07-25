using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        [Required]
        [KeyGuidLength]
        public string Id { get; set; }
        protected BaseEntity()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
