
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Controllers;

[ApiController]
[Route("api/habit-tracker")]
public class HabitController : ControllerBase
{
    private readonly IHabitService habitService;
    public HabitController(IHabitService habitService)
    {
        this.habitService = habitService;

    }

    [HttpPost]
    [Route("")]
    async public Task<ActionResult<Habit>> Post([FromBody] Habit habit)
    {
        habit.Id = Guid.NewGuid();
        if (habit == null)
        {
            return BadRequest();
        }
        await this.habitService.AddHabit(habit);
        return Ok();
    }



}
