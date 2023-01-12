namespace Api.Models;

public class DailyMood
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public string? Mood { get; set; }
}