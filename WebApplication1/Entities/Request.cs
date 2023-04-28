using System.ComponentModel.DataAnnotations;
using WebApplication1.Controllers.Entities;

namespace WebApplication1.Entities
{
    public class Request: Audit
    {

        public int Id { get; set; }

        [StringLength(200)]
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public int Plazo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Boolean Enabled { get; set; } = true;

        public int ClientId { get; set; }
        public Client  Client { get; set; }


        public int RequestTypeId { get; set; }
        public RequestType RequestType { get; set; }


        public int AdvisorId { get; set; }
        public User Advisor { get; set; }


        public int StatusId { get; set; }
        public Status Status { get; set; }

    }
}
