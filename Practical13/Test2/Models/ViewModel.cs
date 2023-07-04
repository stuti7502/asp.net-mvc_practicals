using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Test2.Models
{
    public class ViewModel
    {
        public Employee2 employee2 { get; set; }
        public Designation designation { get; set; }
        public int count { get; set; }
        [DisplayName("Designation")]
        public string desg { get; set; }
    }
}