using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities.Address
{
    public class City : BaseEntity
    {
        [Required]
        [MaxLength(25)]
        public string? CityName { get; set; } = null!;
        public ICollection<District>? Districts { get; set; }
        public City(string cityName)
        {
            CityName = cityName;
        }
    }
}
