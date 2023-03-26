using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using Api.Repositories;
using Api.Services;
using Api.Controllers;
using System.Configuration;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<ApiDbContext>(options =>
                    options.UseMySql(connectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.8.3-mariadb")));
            builder.Services.AddScoped<IUtility, Utility>();
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
            builder.Services.AddScoped<IHabitRepository, HabitRepository>();
            builder.Services.AddScoped<IHabitService, HabitService>();
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApiDbContext>();
            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews();
            builder.Services.AddControllersWithViews().AddNewtonsoftJson();

            builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                using (var scope = app.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;

                    var context = services.GetRequiredService<ApiDbContext>();
                    if (context.Database.GetPendingMigrations().Any())
                    {
                        context.Database.Migrate();
                    }
                }
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}"
            );

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}

