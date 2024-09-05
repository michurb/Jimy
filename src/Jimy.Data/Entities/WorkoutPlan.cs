namespace Jimy.Data.Entities;

public class WorkoutPlan
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; }
}