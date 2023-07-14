using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Practical19.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Practical19.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):
            base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
