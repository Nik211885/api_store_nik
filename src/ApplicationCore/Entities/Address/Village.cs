using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities.Address
{
    public class Village : BaseEntity
    {
        [Required]
        [MaxLength(25)]
        public string NameVillage { get; set; } = null!;
        [Required]
        [KeyGuidLength]
        public string DistrictId { get; private set; } = null!;
        public District? District { get; set; }
        public ICollection<AddressState>? Addresses { get; set; }
        public Village(string nameVillage, string districtId)
        {
            NameVillage = nameVillage;
            DistrictId = districtId;
        }

    }
}
