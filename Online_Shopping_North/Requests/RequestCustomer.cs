using System.ComponentModel.DataAnnotations;

namespace Online_Shopping_North.Requests
{
    public class RequestCustomer
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Email is invalid")]
        public string? Email { get; set; }
        [Required]
        [MinLength(6)]
        public string? Password { get; set; }
        [MaxLength(10)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Phone number must contain only digits.")]
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public int Year {  get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        [Required]
        public string RegionId { get; set; } = null!;
        public Guid CityId { get; set; }
        public Guid DistrictId { get; set; }
        [Required]
        public string Street { get; set; } = null!;

    }
}
