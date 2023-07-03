using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test3.Models
{
    public class Designation
    {
        public int designationId { get; set; }
        public string DesignationName { get; set;}
        public Employee EmployeeData { get; set; }
    }
}