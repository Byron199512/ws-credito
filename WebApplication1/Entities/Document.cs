using System.ComponentModel.DataAnnotations;
using WebApplication1.Controllers.Entities;

namespace WebApplication1.Entities
{
    public class DocumentClient: Audit
    {
        [Key]
        [StringLength(15)]
        public string Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }
        public List<Client> clients { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidUntil { get; set; }
    
    }
}
