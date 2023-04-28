using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public class RequestTypeDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        public string Name { get; set; }
        [Required]
        public float MinAmount { get; set; }
        [Required]
        public float MaxAmount { get; set; }
        [Required]
        public string Term { get; set; }
    }
}
