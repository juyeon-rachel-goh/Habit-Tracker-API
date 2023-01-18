using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services;


public interface IHabitService

{
    public Task<IList<Habit>> GetHabits();
    public Task<IList<DailyMood>> GetDailyMoods();
    public Task AddHabit(Habit habit);
    public Task AddMood(DailyMood mood);
    public Task UpdateMood(Guid id, DailyMood mood);
    public Task DeleteMood(DailyMood mood);

}