using Jimy.Application.Abstraction;
using Jimy.Application.DTO;

namespace Jimy.Application.Queries.WorkoutPlans;

public class GetWorkoutSession : IQuery<WorkoutSessionDto>
{
    public Guid WorkoutSessionId { get; set; }
}