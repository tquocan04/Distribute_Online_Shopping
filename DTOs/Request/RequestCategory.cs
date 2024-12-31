using DTOs.DTOs;
using System.ComponentModel.DataAnnotations;

namespace DTOs.Request
{
    public class RequestCategory
    {
        [Required]
        public string? Name { get; set; }
    }
}
