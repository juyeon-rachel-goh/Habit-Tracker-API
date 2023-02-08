using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using Api.Repositories;
using Api.Services;
using Api.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
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
