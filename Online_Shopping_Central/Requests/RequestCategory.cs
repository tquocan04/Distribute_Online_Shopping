using System.ComponentModel.DataAnnotations;

namespace Online_Shopping_Central.Requests
{
    public class RequestCategory
    {
        [Required]
        public string? Name { get; set; }
    }
}
