using System.ComponentModel.DataAnnotations;

namespace Entities.Entities
{
    public class Region
    {
        [Required]
        public string Id { get; set; } = null!;
        [Required]
        public string? Name { get; set; }
        public ICollection<City>? Cities { get; set; }
    }
}
