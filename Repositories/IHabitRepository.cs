using Api.Models;

namespace Api.Repositories;

public interface IHabitRepository
{
    public Task<DailyMood> GetDailyMoodbyID(Guid id);
    public Task<Habit> GetHabitbyID(Guid id);
    public Task<DailyHabitRecord> GetRecordbyId(Guid id);
    public Task<bool> BeUniqueHabit(Habit habit);
    public Task<bool> BeUniqueDailyMood(DailyMood mood);

}