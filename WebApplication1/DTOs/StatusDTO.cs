using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public class StatusDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string Name { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Description { get; set; }

    }
}
