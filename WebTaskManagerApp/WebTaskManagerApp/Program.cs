using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Events;
using Serilog;

namespace WebTaskManagerApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<TaskManagerContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("TaskManagerContext") ?? throw new InvalidOperationException("Connection string 'TaskManagerContext' not found.")));
            
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(90);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            Log.Logger = new LoggerConfiguration()
            .WriteTo.File(
                string.Format("{0:yyyy}/{0:MM}-{0:dd}/WebLog.txt", DateTime.Now),
                LogEventLevel.Information,
                "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
            )
            .CreateLogger();

            builder.Host.UseSerilog();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            
            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/User/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=User}/{action=Login}/{id?}");

            app.Run();
        }
    }
}