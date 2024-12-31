using Entities.Entities;
using System.ComponentModel.DataAnnotations;

namespace DTOs.Request
{
    public class RequestLogin
    {
        [Required(ErrorMessage = "This field cannot null")]
        public string Login { get; set; } = null!;
        [Required(ErrorMessage = "Password cannot null")]
        [MinLength(6)]
        public string Password { get; set; } = null!;
    }
}
