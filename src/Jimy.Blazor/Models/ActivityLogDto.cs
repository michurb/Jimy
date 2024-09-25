namespace Jimy.Blazor.Models;

public class ActivityLogDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
    public string ActivityType { get; set; }
    public int Duration { get; set; }
    public Guid? WorkoutPlanId { get; set; }
    public string? WorkoutPlanName { get; set; }
}