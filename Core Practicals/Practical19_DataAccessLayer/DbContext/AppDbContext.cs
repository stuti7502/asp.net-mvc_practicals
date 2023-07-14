using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practical19_DataAccessLayer.DbContext
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions options): base(options)
        {

        }
    }
}
