using System.ComponentModel.DataAnnotations;

namespace Entities.Entities
{
    public class ShippingMethod
    {
        [Required]
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public ICollection<Order>? Oders { get; set; }
    }
}
