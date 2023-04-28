using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public class LoginDTO
    {
        [Required]
        [MinLength(5)]
        public string UserName { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
