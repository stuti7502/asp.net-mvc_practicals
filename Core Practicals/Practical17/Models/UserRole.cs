using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practical17.Models
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("user")]
        public int UserId { get; set; }
        [ForeignKey("roles")]
        public int RoleId { get; set; }
        public User user { get; set; }
        public Roles roles { get; set; }
    }
}
