using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public class RolDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(5)]

        public string Name { get; set; }


        public Boolean IsActive { get; set; } = true;
    }
}
