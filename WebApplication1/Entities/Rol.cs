using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Entities
{
    public class Rol: Audit
    {
        public int Id { get; set; }
        [StringLength(20)]
        public string Name { get; set; }

        public Boolean IsActive { get; set; } = true;

        public  List<User> Users { get; set; }

    }
}
