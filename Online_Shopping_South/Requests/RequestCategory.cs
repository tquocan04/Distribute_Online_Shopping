using System.ComponentModel.DataAnnotations;

namespace Online_Shopping_South.Requests
{
    public class RequestCategory
    {
        [Required]
        public string? Name { get; set; }
    }
}
