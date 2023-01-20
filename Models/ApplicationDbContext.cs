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

}
