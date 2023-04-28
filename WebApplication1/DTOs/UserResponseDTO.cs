using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public class UserResponseDTO
    {
        [Required]
        [MinLength(4)]
        public string Name { get; set; }
        [Required]
        [MinLength(4)]
        public string LastName { get; set; }
        [Required, EmailAddress]
        [MinLength(4)]
        public string Email { get; set; }
        
        public string UserName { get; set; }
        [Required]
        [MinLength(6)]

        public int RolId { get; set; }
    }
}
