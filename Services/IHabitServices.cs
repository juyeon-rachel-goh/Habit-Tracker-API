using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services;


public interface IHabitService
{
    public Task<IList<Habit>> GetHabits(string currentUserId);
    public Task AddHabit(Habit habit);

}