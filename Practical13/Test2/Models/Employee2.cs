using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test2.Models
{
    public class Employee2
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [StringLength(50)]
        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        [Required]
        [StringLength(10)]
        public string Mobile_Number { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        [Required]
        public int Salary { get; set; }
        [ForeignKey("Designation")]
        public int DesignationID { get; set; }
        public Designation Designation { get; set; }
    }
}