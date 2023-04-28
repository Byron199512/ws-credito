


namespace WebApplication1.Entities
{
    public class User: Audit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Boolean IsActive { get; set; } = true;

        public int RolId { get; set; }
        public  Rol Rol { get; set; }


        public List<Request> Requests { get; set; }

    }
}
