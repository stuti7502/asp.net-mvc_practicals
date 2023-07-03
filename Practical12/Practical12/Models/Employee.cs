using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Practical12.Models
{
    [Table("employee")]
    public class Employee
    {
            [Key]
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string LastName { get; set; }
            public DateTime DOB { get; set; }
            public int MobileNumber {get; set; }
            public string address { get; set; } 
        
        }
    
}