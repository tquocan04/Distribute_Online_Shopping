using Entities.Entities;

namespace DTOs.DTOs
{
    public class CityDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ICollection<DistrictDTO>? Districts { get; set; }
    }
}
