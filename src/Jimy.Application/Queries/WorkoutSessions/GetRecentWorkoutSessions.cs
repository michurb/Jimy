using Jimy.Application.Abstraction;
using Jimy.Application.DTO;

namespace Jimy.Application.Queries.WorkoutPlans;

public class GetRecentWorkoutSessions : IQuery<IEnumerable<WorkoutSessionDto>>
{
    public Guid UserId { get; set; }
    public int  Count { get; set; } = 5;
}