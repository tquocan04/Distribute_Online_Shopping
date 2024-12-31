using System.ComponentModel.DataAnnotations;

namespace DTOs.Request
{
    public class RequestSignupGoogle
    {
        [Required]
        public string? name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Email is invalid")]
        public string? email { get; set; }
        public string? password { get; set; }
        [MaxLength(10)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Phone number must contain only digits.")]
        public string? phonenumber { get; set; }
        public string? gender { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
        public string regionId { get; set; } = null!;
        public Guid cityId { get; set; }
        public Guid districtId { get; set; }
        public string street { get; set; } = null!;
        public string? picture { get; set; }
        public string googleId { get; set; } = null!;
    }
}
