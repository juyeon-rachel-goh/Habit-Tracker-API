using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class HabitRepository : IHabitRepository
{
    private ApiDbContext context;
    public HabitRepository(ApiDbContext context)
    {
        this.context = context;
    }

    async public Task<bool> FindExistingMood(DailyMood mood)
    {
        var result = await this.context.DailyMoods
        .Where(moodData => moodData.EventDate == mood.EventDate && moodData.IdentityUserID == mood.IdentityUserID)
        .CountAsync();
        if (result == 1)
        {
            return true;
        }
        return false;
    }

    async public Task<bool> BeUniqueDailyMood(DailyMood mood)
    {
        var result = await this.context.DailyMoods
        .Where(moodData => moodData.EventDate == mood.EventDate && moodData.IdentityUserID == mood.IdentityUserID)
        .CountAsync();
        if (result == 1)
        {
            return true;
        }
        return false;
    }

    // Return DailyMoodID
    async public Task<Guid> FindDailyMoodId(DailyMood mood)
    {
        var result = await this.context.DailyMoods
        .AsNoTracking()
        .Where(moodData => moodData.EventDate == mood.EventDate && moodData.IdentityUserID == mood.IdentityUserID)
        .FirstAsync();
        return result.Id;
    }

    async public Task<DailyMood> GetDailyMoodbyID(Guid id)
    {
        return await this.context.DailyMoods
        .AsNoTracking()
        .Where(moodData => moodData.Id == id)
        .FirstAsync();
    }

    async public Task<Habit> GetHabitbyID(Guid id)
    {
        return await this.context.Habits
        .AsNoTracking()
        .Where(habit => habit.Id == id)
        .FirstAsync();
    }
}
