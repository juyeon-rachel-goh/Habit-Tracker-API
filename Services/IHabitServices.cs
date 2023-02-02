using Api.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services;


public interface IHabitService

{
    public Task<IList<Habit>> GetHabits(string currentUser);
    public Task<IList<DailyMood>> GetDailyMoods(string currentUser);
    public Task<IList<DailyHabitRecord>> GetCompletionStatus(string currentUser);
    public Task AddHabit(Habit habit);
    public Task AddMood(DailyMood mood);
    public Task AddNewDailyRecord(DailyHabitRecord record);
    public Task UpdateMood(DailyMood mood); //dont need id here if 
    public Task UpdateHabit(Habit habit);
    public Task PatchArchiveStatus(Habit habit, JsonPatchDocument value);
    public Task DeleteHabit(Habit habit);
    public Task DeleteMood(DailyMood mood);
    public Task DeleteRecord(DailyHabitRecord record);
    public Task ArchiveHabit(JsonPatchDocument<Habit> patchDoc, Habit habit);

}