using Practical20.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Practical20.Models
{
    public class Student :  IAuditable
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Address { get; set; }
        public int? Age { get; set; }
    }
}
