using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Api.Models;

public class DailyHabitRecord
{
    public Guid Id { get; set; }
    public string Date { get; set; }

    public bool CompletionStatus { get; set; }

    public Guid HabitId { get; set; }

    public Habit? Habits { get; set; }

    [ForeignKey("IdentityUser")]
    public string? IdentityUserID { get; set; }

    public IdentityUser? IdentityUser { get; set; }
}
