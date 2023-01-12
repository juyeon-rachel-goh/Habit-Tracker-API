namespace Api.Models;

using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

public class DailyHabitRecord
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public string Mood { get; set; }
    public bool CompletionStatus { get; set; }

    public Guid HabitId { get; set; } //Foreign Key - one habit many dailyhabitrecords

    public Habit? Habits { get; set; }
}
