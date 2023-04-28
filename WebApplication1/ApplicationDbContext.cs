using Microsoft.EntityFrameworkCore;
using WebApplication1.Controllers.Entities;
using WebApplication1.Entities;

namespace WebApplication1
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
      


        //Configure all tables

        public DbSet<DocumentClient> Documents { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Status> Status { get; set; }

        public DbSet<RequestType> RequestTypes { get; set; }

        public DbSet<Request> Requests { get; set; }


    }
}
