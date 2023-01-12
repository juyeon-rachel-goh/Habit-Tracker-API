using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services;


public interface IHabitService

{
    public Task<IList<Habit>> GetHabits();
    public Task AddHabit(Habit habit);

}