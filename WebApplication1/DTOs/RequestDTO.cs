using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public class RequestDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        [MinLength(2)]
        public string Description { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public int Plazo { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string documentId { get; set; }
        public int ClientId { get; set; }

        [Required]
        public int RequestTypeId { get; set; }

        public int AdvisorId { get; set; }
        public int StatusId { get; set; }
    }
}
