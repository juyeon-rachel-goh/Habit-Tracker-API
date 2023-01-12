using Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class ApiDbContext : IdentityDbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options)
        : base(options)
    {

    }

    public DbSet<Habit> Habits { get; set; }

    public DbSet<DailyHabitRecord> DailyHabitRecords { get; set; }
    public DbSet<DailyMood> DailyMoods { get; set; }
    //     protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder
    //         .Entity<Habit>()
    //         .HasOne(e => e.IdentityUser).WithOne(e => e.Id)

    // }  
}
