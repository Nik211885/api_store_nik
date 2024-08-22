using ApplicationCore.Entities.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities.Ratings
{
    public class Reaction
    {
        /// <summary>
        ///  If true is like and false is unlike otherwise deleted record
        /// </summary>
        /// 
        [Required]
        [KeyGuidLength]
        public string UserId { get; set; } = null!;
        public bool Like { get; set; }
        [Required]
        [KeyGuidLength]
        public string RatingId { get; private set; } = null!;
        public Rating? Rating { get; private set; } = null!;
        public DateTime DateReaction { get; private set; }
        public Reaction(bool like, string ratingId, string userId)
        {
            Like = like;
            RatingId = ratingId;
            DateReaction = DateTime.Now;
            UserId = userId;
        }
    }
}
