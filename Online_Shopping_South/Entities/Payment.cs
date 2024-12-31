using System.ComponentModel.DataAnnotations;

namespace Online_Shopping_South.Entities
{
    public class Payment
    {
        [Required]
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Image { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
