using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Api.Services;

public class HabitService : IHabitService

{
    private ApiDbContext context;

    private UserManager<IdentityUser> _userManager;
    public HabitService(ApiDbContext context, UserManager<IdentityUser> userManager)
    {
        this.context = context;
        _userManager = userManager;
    }

    async public Task<IList<Habit>> GetHabits()
    {
        return await this.context.Habits.ToListAsync();
    }
    async public Task AddHabit(Habit habit)
    {
        var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            await context.AddAsync(habit);
            await context.SaveChangesAsync();

            await context.Database.CommitTransactionAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
        }
    }


}