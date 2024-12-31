using System.ComponentModel.DataAnnotations;

namespace Online_Shopping_Central.DTOs
{
    public class EmployeeDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        [EmailAddress(ErrorMessage = "Email is invalid")]
        public string? Email { get; set; }
        [MinLength(6)]
        public string? Password { get; set; }
        [MaxLength(10)]
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? Day { get; set; }
        public string RoleId { get; set; } = null!;
        public Guid BranchId { get; set; }
        public string? BranchName { get; set; }
        public string RegionId { get; set; } = null!;
        public string? RegionName { get; set; }
        public Guid CityId { get; set; }
        public string? CityName { get; set; }
        public Guid DistrictId { get; set; }
        public string? DistrictName { get; set; }
        public string Street { get; set; } = null!;
    }
}
