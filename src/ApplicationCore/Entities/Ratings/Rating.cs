using ApplicationCore.Entities.Attributes;
using ApplicationCore.Entities.Order;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities.Ratings
{
    public class Rating : BaseEntity
    {
        [KeyGuidLength]
        [Required]
        public string OrderDetailId { get; private set; } = null!;
        public OrderDetail? OrderDetail { get; set; }
        public int Start { get; set; }
        public string? CommentRating { get; set; }
        public DateTime DateRating { get; private set; }
        public ICollection<Reaction>? Reactions { get; set; }
        public Rating(string orderDetailId, int start, string? commentRating)
        {
            OrderDetailId = orderDetailId;
            Start = start;
            CommentRating = commentRating;
            DateRating = DateTime.Now;
        }
    }
}
