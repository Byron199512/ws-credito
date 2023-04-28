using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Entities
{
    public class RequestType:Audit
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public float MinAmount { get; set; }
        public float MaxAmount { get; set; }
        public string Term { get; set; }

        public List<Request> Requests { get; set; }
    }
}
