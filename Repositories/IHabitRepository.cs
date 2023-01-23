using Api.Models;

namespace Api.Repositories;

public interface IHabitRepository
{
    public Task<bool> BeUniqueDailyMood(DailyMood mood);

    public Task<Guid> FindDailyMoodId(DailyMood mood);

    public Task<DailyMood> GetDailyMoodbyID(Guid id);

    public Task<Habit> GetHabitbyID(Guid id);

    public Task<bool> BeUniqueDailyHabitRecord(DailyHabitRecord record);
    public Task<Guid> FindDailyHabitRecordId(DailyHabitRecord record);
    public Task<bool> FindCurrentCompletionStatus(DailyHabitRecord record);

}