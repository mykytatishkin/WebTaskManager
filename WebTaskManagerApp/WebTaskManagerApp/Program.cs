using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace WebTaskManagerApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<TaskManagerContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("TaskManagerContext") ?? throw new InvalidOperationException("Connection string 'TaskManagerContext' not found.")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/User/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=User}/{action=Login}/{id?}");

            app.Run();
        }
    }
}