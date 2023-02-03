namespace Api.Models;

using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

public class Habit
{
    public Guid Id { get; set; }
    public string? HabitName { get; set; }
    public string? Frequency { get; set; }
    public string CountPerFreq { get; set; }
    public string IconColor { get; set; }
    public DateTime CreatedOn { get; set; }
    public bool ArchiveStatus { get; set; }
    [JsonIgnore]
    [ForeignKey("IdentityUser")]
    public string? IdentityUserID { get; set; }
    [JsonIgnore]
    public IdentityUser? IdentityUser { get; set; }

}

