using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;


namespace Api.Services;

public class HabitService : IHabitService

{
    private ApiDbContext context;
    public HabitService(ApiDbContext context)
    {
        this.context = context;
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