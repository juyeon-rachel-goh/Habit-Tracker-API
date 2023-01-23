using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services;


public interface IHabitService

{
    public Task<IList<Habit>> GetHabits(string currentUser);
    public Task<IList<DailyMood>> GetDailyMoods(string currentUser);
    public Task AddHabit(Habit habit);
    public Task AddMood(DailyMood mood);
    public Task UpdateMood(Guid id, DailyMood mood);
    public Task DeleteHabit(Habit habit);
    public Task DeleteMood(DailyMood mood);

}