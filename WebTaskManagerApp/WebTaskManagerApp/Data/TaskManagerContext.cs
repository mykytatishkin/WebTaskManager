using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Fluent API
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<User>().HasIndex(u => u.Name).IsUnique();
    }
}
