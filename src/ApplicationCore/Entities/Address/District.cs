using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities.Address
{
    public class District : BaseEntity
    {
        [Required]
        [MaxLength(35)]
        public string DistrictName { get; set; } = null!;
        [Required]
        [KeyGuidLength]
        public string CityId { get; private set; } = null!;
        public City? City { get; set; }
        public ICollection<Village>? Villages { get; set; }
        public District(string cityId, string districtName)
        {
            CityId = cityId;
            DistrictName = districtName;
        }
    }
}
