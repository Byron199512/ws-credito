using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public class ClientDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        public string FullName { get; set; }

        public string DocumentId { get; set; }

        public DocumentDTO document { get; set; }
    }
}
