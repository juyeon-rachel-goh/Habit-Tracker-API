using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Api.Models;

public class DailyMood
{
    public Guid Id { get; set; }
    public string EventDate { get; set; }
    public string? Mood { get; set; }

    [ForeignKey("IdentityUser")]
    public string? IdentityUserID { get; set; }

    public IdentityUser? IdentityUser { get; set; }
}