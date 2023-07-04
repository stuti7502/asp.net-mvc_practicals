using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Practical13.Models
{
    public class EmployeeContext: DbContext
    {
        public EmployeeContext(): base("name=ConnectionString") { 
        }
        public DbSet<Employee> Employees { get; set;}
    }
}