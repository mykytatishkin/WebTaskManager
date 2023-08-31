using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebTaskManagerApp.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Competed { get; set; }
        [ForeignKey("FK_PROJECT_456")]
        public int ProjectId { get; set; }
        [ForeignKey("FK_USER_456")]
        public int AsigneeId { get; set; }
    }
}
