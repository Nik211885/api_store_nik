using ApplicationCore.Entities.Products;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities.Ratings
{
    public class Rating : BaseEntity
    {
        [KeyGuidLength]
        [Required]
        public string UserId { get; private set; } = null!;
        [KeyGuidLength]
        [Required]
        public string ProductId { get; private set; } = null!;
        public Product? Product { get; set; }
        [Required]
        public float Start { get; set; }
        public string? CommentRating { get; set; }
        public DateTime DateRating { get; private set; }
        public ICollection<Reaction>? Reactions { get; set; }
        public Rating(string userId, string productId, float start, string commentRating)
        {
            UserId = userId;
            ProductId = productId;
            Start = start;
            CommentRating = commentRating;
            DateRating = DateTime.Now;
        }
    }
}
