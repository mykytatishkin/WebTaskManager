using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebTaskManagerApp.Models;

    public class TaskManagerContext : DbContext
    {
        public TaskManagerContext (DbContextOptions<TaskManagerContext> options)
            : base(options)
        {
        }

        public DbSet<WebTaskManagerApp.Models.Role> Roles { get; set; } = default!;
        public DbSet<WebTaskManagerApp.Models.User> Users { get; set; } = default!;
        public DbSet<WebTaskManagerApp.Models.Project> Project { get; set; } = default!;
        public DbSet<WebTaskManagerApp.Models.Task> Tasks { get; set; } = default!;
    }
