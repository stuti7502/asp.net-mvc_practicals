using System.ComponentModel.DataAnnotations;

namespace Practical17.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [MaxLength(10)]
        public string Phone { get; set; }
        [Required]
        public int RolesId { get; set; }
        public virtual Roles Roles { get; set; }
    }
}
