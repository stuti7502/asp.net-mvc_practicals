using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Test2.Models;

namespace Practical13.Models
{
    public class EmployeeContext: DbContext
    {
        public EmployeeContext(): base("name=ConnectionString") { 
        }
        public DbSet<Employee2> Employees { get; set;}
        public DbSet<Designation> Designations { get; set;}
    }
}