using System.ComponentModel.DataAnnotations;

namespace DTOs.Request
{
    public class RequestBranch
    {
        public string? Name { get; set; }
        [MaxLength(10)]
        public string? PhoneNumber { get; set; }
        public string RegionId { get; set; } = null!;
        public Guid CityId { get; set; }
        public Guid DistrictId { get; set; }
        public string Street { get; set; } = null!;
    }
}
