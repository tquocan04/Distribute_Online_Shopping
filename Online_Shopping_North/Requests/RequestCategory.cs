using System.ComponentModel.DataAnnotations;

namespace Online_Shopping_North.Requests
{
    public class RequestCategory
    {
        [Required]
        public string? Name { get; set; }
    }
}
