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

    async public Task<IList<Habit>> GetHabits(string currentUser)
    {
        return await this.context.Habits.Where(habit => habit.IdentityUserID == currentUser).ToListAsync();
    }


    async public Task<IList<DailyMood>> GetDailyMoods(string currentUser)
    {
        return await this.context.DailyMoods.Where(mood => mood.IdentityUserID == currentUser).ToListAsync();
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

    async public Task AddMood(DailyMood mood)
    {
        var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            await context.AddAsync(mood);
            await context.SaveChangesAsync();

            await context.Database.CommitTransactionAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
        }
    }

    async public Task UpdateMood(Guid id, DailyMood mood)
    {
        mood.Id = id;
        context.DailyMoods.Update(mood);

        await context.SaveChangesAsync();

    }

    async public Task DeleteMood(DailyMood mood)
    {
        var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            context.DailyMoods.Remove(mood);
            await context.SaveChangesAsync();

            await context.Database.CommitTransactionAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
        }
    }
}