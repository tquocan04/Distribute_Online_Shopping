using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Online_Shopping_Central.Entities
{
    public class Credential
    {
        [Required]
        public string Id { get; set; } = null!;
        public string? Provider { get; set; }
        [ForeignKey(nameof(Customer))]
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}