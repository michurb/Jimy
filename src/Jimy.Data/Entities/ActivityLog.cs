namespace Jimy.Data.Entities;

public class ActivityLog
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime Date { get; set; }
    public string ActivityType { get; set; }
    public int Duration { get; set; }
    public int? WorkoutPlanId { get; set; }
}