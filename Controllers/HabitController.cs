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

    [HttpGet]
    [Route("records")]
    async public Task<IList<DailyHabitRecord>> GetCompletionStatus()
    {
        var currentUser = (await this.utility.GetContextUser(HttpContext)).Id;
        return await this.habitService.GetCompletionStatus(currentUser);
    }

    [HttpPut] // Changed from POST -> PUT (upserting)
    [Route("upsert-habit")]
    async public Task<ActionResult<Habit>> AddHabit([FromBody] Habit habit)
    {
        var currentUser = (await this.utility.GetContextUser(HttpContext)).Id;
        habit.IdentityUserID = currentUser;

        if (habit == null)
        {
            return BadRequest();
        }

        var result = await this.habitRepository.BeUniqueHabit(habit);
        if (result) //if true
        {
            var id = habit.Id;
            await this.habitService.UpdateHabit(habit);
            return Ok();
            //need error handling
        }
        else
        {
            habit.Id = Guid.NewGuid();
            habit.ArchiveStatus = false;
            habit.CreatedOn = DateTime.Now;
            await this.habitService.AddHabit(habit);
        }

        return Ok();
    }


    [HttpPatch]
    [Route("archive/{id:Guid}")]
    async public Task<ActionResult<Habit>> archiveHabit([FromBody] JsonPatchDocument<Habit> patchDoc, Guid id)
    { // DTO like username in Auth (more specific way to patch)
        var habit = await habitRepository.GetHabitbyID(id);
        await habitService.ArchiveHabit(patchDoc, habit);
        return Ok();
    }

    // Upserting Mood Data 
    [HttpPut]
    [Route("mood/upsert")]
    async public Task<ActionResult<DailyMood>> AddMood([FromBody] DailyMood mood)
    {

        var currentUser = (await this.utility.GetContextUser(HttpContext)).Id;
        // 
        mood.IdentityUserID = currentUser;

        if (mood == null)
        {
            return BadRequest();
        }

        var result = await this.habitRepository.BeUniqueDailyMood(mood);
        if (result) // true -> update existing record
        {
            var id = mood.Id;

            await this.habitService.UpdateMood(mood);
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

    [HttpPost]
    [Route("records/complete")] //update completion status
    async public Task<ActionResult<DailyHabitRecord>> AddDailyRecord([FromBody] DailyHabitRecord record)
    {

        var currentUser = (await this.utility.GetContextUser(HttpContext)).Id;
        // compare ID before hitting respository
        record.IdentityUserID = currentUser;


        if (record == null)
        {
            return BadRequest();
        }

        record.Id = Guid.NewGuid();
        record.CompletionStatus = true;
        await this.habitService.AddNewDailyRecord(record);
        return Ok();
        //need error handling
    }



    [HttpDelete]
    [Route("delete-habit/{id:Guid}")]
    async public Task<ActionResult<Habit>> DeleteHabit(Guid id)
    {
        //user ID checks before running below 
        var habit = await habitRepository.GetHabitbyID(id);
        if (habit == null)
        {
            return NotFound();
        }
        await this.habitService.DeleteHabit(habit);
        return Ok();
    }
    [HttpDelete]
    [Route("mood/delete/{id:Guid}")]
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

    [HttpDelete]
    [Route("records/delete/{id:Guid}")]
    async public Task<ActionResult<Habit>> DeleteRecord(Guid id)
    {
        var record = await habitRepository.GetRecordbyId(id);
        if (record == null)
        {
            return NotFound();
        }
        await this.habitService.DeleteRecord(record);
        return Ok();
    }

}

