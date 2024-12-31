using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Online_Shopping_Central.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [MinLength(6)]
        public string? Password { get; set; }
        [MaxLength(11)]
        public string? PhoneNumber { get; set; }
        public DateOnly Dob { get; set; }
        public string? Picture { get; set; }
        [JsonIgnore]
        public ICollection<Address>? Addresses { get; set; }
        [JsonIgnore]
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Credential>? Credentials { get; set; }
    }
}