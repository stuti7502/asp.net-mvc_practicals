using System.ComponentModel.DataAnnotations;

namespace Practical17.Models
{
    public class Roles
    {
        [Key]
        public int RolesId { get; set; }
        [Required]
        public string RoleName { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
