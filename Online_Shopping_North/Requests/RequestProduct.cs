using System.ComponentModel.DataAnnotations;

namespace Online_Shopping_North.Requests
{
    public class RequestProduct
    {
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public int? Stock { get; set; }
        public IFormFile? Image { get; set; }
        public Guid CategoryId { get; set; }
    }
}
