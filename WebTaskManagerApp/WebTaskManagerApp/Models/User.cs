using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebTaskManagerApp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? Avatar { get; set; }
        [ForeignKey("FK_ROLE_123")]
        public int RoleId { get; set; }
    }
}
