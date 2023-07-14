using Microsoft.EntityFrameworkCore;

namespace Practical17.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :
            base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        
        
    }
}
