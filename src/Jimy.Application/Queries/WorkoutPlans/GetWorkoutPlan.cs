using Jimy.Application.Abstraction;
using Jimy.Application.DTO;

namespace Jimy.Application.Queries.WorkoutPlans;

public class GetWorkoutPlan : IQuery<WorkoutPlanDto>
{
    public Guid WorkoutPlanId { get; set; }
}