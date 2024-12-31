using System.ComponentModel.DataAnnotations;

namespace Online_Shopping_Central.DTOs
{
    public class CustomerDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [MinLength(6)]
        public string? Password { get; set; }
        [MaxLength(10)]
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? Day { get; set; }
        public string? Picture { get; set; }
        public string RegionId { get; set; } = null!;
        public Guid CityId { get; set; }
        public Guid DistrictId { get; set; }
        public string Street { get; set; } = null!;
    }
}
