using Entities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTOs.DTOs
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
        public string? Image { get; set; }
        public string? CategoryName { get; set; }
    }
}
