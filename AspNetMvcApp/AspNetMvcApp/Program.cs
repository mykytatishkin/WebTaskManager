using AspNetMvcApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

// Configure the DbContext and add it to the services.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); // Use the connection string name defined in your appsettings.json
services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add support for Entity Framework migrations
// services.AddDatabaseDeveloperPageExceptionFilter();

services.AddControllersWithViews();

var app = builder.Build();

// Apply database migrations (create/update the database)
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    dbContext.Database.EnsureCreated();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
