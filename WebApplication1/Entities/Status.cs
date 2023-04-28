using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Entities
{
    public class Status:Audit
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Description { get; set; }


        public List<Request> Requests { get; set; }
    }
}
