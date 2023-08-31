using System.ComponentModel.DataAnnotations;

namespace WebTaskManagerApp.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string RoleName { get; set; }
    }
}
