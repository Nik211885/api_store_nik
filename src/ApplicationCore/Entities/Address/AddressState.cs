using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities.Address
{
    public class AddressState : BaseEntity
    {
        [KeyGuidLength]
        [Required]
        public string UserId { get; private set; } = null!;
        [MaxLength(100)]
        public string AddressDetail { get; set; } = null!;
        [Required]
        [KeyGuidLength]
        public string VillageId { get; private set; } = null!;
        public Village? Village { get; set; }
        public AddressState(string userId, string addressDetail, string villageId)
        {
            VillageId = villageId;
            UserId = userId;
            AddressDetail = addressDetail;
        }
    }
}
