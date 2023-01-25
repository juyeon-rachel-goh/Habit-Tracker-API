using Api.Models;
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
    public Task UpdateMood(Guid id, DailyMood mood); //dont need id here if 
    public Task UpdateHabit(Habit habit);
    public Task UpdateDailyHabitRecord(Guid id, DailyHabitRecord record);
    public Task DeleteHabit(Habit habit);
    public Task DeleteMood(DailyMood mood);

}