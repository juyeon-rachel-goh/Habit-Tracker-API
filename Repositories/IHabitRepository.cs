using Api.Models;

namespace Api.Repositories;

public interface IHabitRepository
{
    public Task<bool> BeUniqueDailyMood(DailyMood mood);

    public Task<Guid> FindDailyMoodId(DailyMood mood);

}