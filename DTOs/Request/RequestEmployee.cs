using System.ComponentModel.DataAnnotations;

namespace DTOs.Request
{
    public class RequestEmployee
    {
        public string? Name { get; set; }
        public string? Username { get; set; }
        [EmailAddress(ErrorMessage = "Email is invalid")]
        public string? Email { get; set; }
        [MinLength(6)]
        public string? Password { get; set; }
        [MaxLength(10)]
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        [Required]
        public string RegionId { get; set; } = null!;
        public Guid CityId { get; set; }
        public Guid DistrictId { get; set; }
        [Required]
        public string Street { get; set; } = null!;
        public string RoleId { get; set; } = null!;
        public Guid BranchId { get; set; }
    }
}
