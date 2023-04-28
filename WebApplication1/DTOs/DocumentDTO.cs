using System.ComponentModel.DataAnnotations;
using WebApplication1.Controllers.Entities;

namespace WebApplication1.DTOs
{
    public class DocumentDTO
    {
        [Required]
        [MinLength(10)]
        [MaxLength(15)]
        public string Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public DateTime ValidFrom { get; set; }
        [Required]
        public DateTime ValidUntil { get; set; }
    }
}
