using System;
namespace AspNetMvcApp.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsCompleted { get; set; }
        // Связь с проектом и пользователями (например, ответственным)
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int AssigneeId { get; set; }
        public User Assignee { get; set; }
        // Дополнительные поля по необходимости
    }

}

