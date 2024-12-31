using System.ComponentModel.DataAnnotations;

namespace DTOs.MongoDb
{
    public class CustomerMetadata
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [MinLength(6)]
        public string? Password { get; set; }
        [MaxLength(10)]
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public DateOnly Dob { get; set; }
        public string? Picture { get; set; }
        public string? RegionName { get; set; }
        public string? CityName { get; set; }
        public string? DistrictName { get; set; }
        public string Street { get; set; } = null!;
    }
}
