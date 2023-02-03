using Api.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services;


public interface IHabitService

{
    public Task<IList<Habit>> GetHabits(string currentUser);
    public Task AddHabit(Habit habit);
    public Task UpdateHabit(Habit habit);
    public Task DeleteHabit(Habit habit);
    public Task ArchiveHabit(JsonPatchDocument<Habit> patchDoc, Habit habit);
    public Task<IList<DailyMood>> GetDailyMoods(string currentUser);
    public Task AddMood(DailyMood mood);
    public Task UpdateMood(DailyMood mood);
    public Task DeleteMood(DailyMood mood);
    public Task<IList<DailyHabitRecord>> GetCompletionStatus(string currentUser);
    public Task AddNewDailyRecord(DailyHabitRecord record);
    public Task DeleteRecord(DailyHabitRecord record);


}