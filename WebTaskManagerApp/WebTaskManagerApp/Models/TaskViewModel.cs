namespace WebTaskManagerApp.Models
{
    public class TaskViewModel
    {
        public Project CurrentProject { get; set; }
        public List<Task> LinkedTasks { get; set; }
    }
}
