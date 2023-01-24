using Api.Repositories;
using Api.DataTransferObjects;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.JsonPatch;

namespace Api.Controllers;

[ApiController]
[Route("api/habit-tracker")]
public class HabitController : ControllerBase
{
    private readonly IHabitService habitService;
    private ILogger<HabitController> _logger;
    private readonly IHabitRepository habitRepository;
    private UserManager<IdentityUser> _userManager;
    private IUtility utility;
    public HabitController(ILogger<HabitController> logger, IHabitService habitService, IHabitRepository habitRepository, UserManager<IdentityUser> userManager, IUtility utility)
    {
        _logger = logger;
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

    [HttpGet]
    [Route("history")]
    async public Task<IList<DailyHabitRecord>> GetCompletionStatus()
    {
        var currentUser = (await this.utility.GetContextUser(HttpContext)).Id;
        return await this.habitService.GetCompletionStatus(currentUser);
    }

    [HttpPost]
    [Route("new-habit")]
    async public Task<ActionResult<Habit>> AddHabit([FromBody] Habit habit)
    {
        habit.Id = Guid.NewGuid();
        habit.ArchiveStatus = false;
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

    [HttpPut]
    [Route("change-habit-record")]
    async public Task<ActionResult<DailyHabitRecord>> ChangeDailyHabitRecord([FromBody] DailyHabitRecord dailyRecord)
    {

        var currentUser = (await this.utility.GetContextUser(HttpContext)).Id;
        dailyRecord.IdentityUserID = currentUser;


        if (dailyRecord == null)
        {
            return BadRequest();
        }

        var result = await this.habitRepository.BeUniqueDailyHabitRecord(dailyRecord);
        if (result)
        {
            var id = await this.habitRepository.FindDailyHabitRecordId(dailyRecord);
            var current = await this.habitRepository.FindCurrentCompletionStatus(dailyRecord);
            dailyRecord.CompletionStatus = !current;

            await this.habitService.UpdateDailyHabitRecord(id, dailyRecord);
            return Ok();
            //need error handling
        }
        else
        {
            dailyRecord.Id = Guid.NewGuid();
            dailyRecord.CompletionStatus = true;
            await this.habitService.AddNewDailyRecord(dailyRecord);
            return Ok();
            //need error handling
        }
    }

    [HttpPatch]
    [Route("change-habit-record/{id:Guid}")]
    async public Task<ActionResult> archiveHabit([FromBody] JsonPatchDocument value, Guid id)
    {
        var habit = await habitRepository.GetHabitbyID(id);
        if (habit == null)
        {
            _logger.LogInformation("getting archive habit request hit - habit null?");
            return BadRequest();

        }
        await this.habitService.PatchArchiveStatus(habit, value);
        _logger.LogInformation("getting archive habit request hit");
        return Ok();
    }
    [HttpDelete]
    [Route("delete-habit/{id:Guid}")]
    async public Task<ActionResult<Habit>> DeleteHabit(Guid id)
    {
        var habit = await habitRepository.GetHabitbyID(id);
        if (habit == null)
        {
            return NotFound();
        }
        await this.habitService.DeleteHabit(habit);
        return Ok();
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

