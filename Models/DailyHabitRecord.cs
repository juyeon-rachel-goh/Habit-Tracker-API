namespace Api.Models;

public class DailyHabitRecord
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }

    public bool CompletionStatus { get; set; }

    public Guid HabitId { get; set; } //Foreign Key - one habit -- many dailyhabitrecords

    public Habit Habits { get; set; }
}
