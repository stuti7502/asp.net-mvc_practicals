using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test3.Models
{
    public class Employee
    {
        [Key]
        public int employeeId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public int MobileNumber { get; set; }
        public string address { get; set; }
        public int salary { get; set; }
        public int DesignationId { get; set; }
        public Designation DesignationData { get; set; }
    }
}