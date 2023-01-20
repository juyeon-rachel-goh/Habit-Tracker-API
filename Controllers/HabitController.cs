using Api.Repositories;
using Api.DataTransferObjects;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Controllers;

[ApiController]
[Route("api/habit-tracker")]
public class HabitController : ControllerBase
{
    private readonly IHabitService habitService;

    private readonly IHabitRepository habitRepository;
    private UserManager<IdentityUser> _userManager;
    private IUtility utility;
    public HabitController(IHabitService habitService, IHabitRepository habitRepository, UserManager<IdentityUser> userManager, IUtility utility)
    {
        this.habitService = habitService;
        this.habitRepository = habitRepository;
        _userManager = userManager;
        this.utility = utility;

    }

    [HttpGet]
    [Route("mood")]
    async public Task<IList<DailyMood>> GetDailyMoods()
    {
        var currentUser = (await this.utility.GetContextUser(HttpContext)).Id;
        return await this.habitService.GetDailyMoods(currentUser);
    }

    [HttpGet]
    [Route("habits")]
    async public Task<IList<Habit>> GetHabits()
    {
        var currentUser = (await this.utility.GetContextUser(HttpContext)).Id;
        return await this.habitService.GetHabits(currentUser);
    }

    [HttpPost]
    [Route("new-habit")]
    async public Task<ActionResult<Habit>> AddHabit([FromBody] Habit habit)
    {
        habit.Id = Guid.NewGuid();
        habit.ArchiveStatus = (ArchiveStatus)0;
        habit.CreatedOn = DateTime.Now;

        var currentUser = (await this.utility.GetContextUser(HttpContext)).Id;
        habit.IdentityUserID = currentUser;


        if (habit == null)
        {
            return BadRequest();
        }
        await this.habitService.AddHabit(habit);
        return Ok();
    }

    // [HttpPatch]
    // [Route("archive/${id: Guid}")]
    // async public Task<ActionResult<Habit>> archiveHabit([FromBody] string value, Guid id)
    // {
    //     var habit = await habitRepository.GetHabitbyID(id);
    // }

    // Upserting Mood Data 
    [HttpPut]
    [Route("update-mood")]
    async public Task<ActionResult<DailyMood>> AddMood([FromBody] DailyMood mood)
    {

        var currentUser = (await this.utility.GetContextUser(HttpContext)).Id;
        mood.IdentityUserID = currentUser;

        if (mood == null)
        {
            return BadRequest();
        }

        var result = await this.habitRepository.BeUniqueDailyMood(mood);
        if (result) // true -> update existing record
        {
            var id = await this.habitRepository.FindDailyMoodId(mood);

            await this.habitService.UpdateMood(id, mood);
            return Ok();
            //need error handling
        }
        else
        {
            mood.Id = Guid.NewGuid();
            await this.habitService.AddMood(mood);
            return Ok();
            //need error handling
        }
    }

    [HttpDelete]
    [Route("delete-mood/{id:Guid}")]
    async public Task<ActionResult<DailyMood>> DeleteMood(Guid id)
    {
        var mood = await habitRepository.GetDailyMoodbyID(id);
        if (mood == null)
        {
            return NotFound();
        }
        await this.habitService.DeleteMood(mood);
        return Ok();
    }

}

