using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using WebApplication1.Entities;
namespace WebApplication1.Controllers.Entities
{
    public class Client: Audit
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string FirstName  { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        [StringLength(201)]
        public string FullName { get; set; }



        public string DocumentId { get; set; }
        public DocumentClient Document { get; set; }


        public  List<Request> Requests { get; set; } 

       
    }
}
