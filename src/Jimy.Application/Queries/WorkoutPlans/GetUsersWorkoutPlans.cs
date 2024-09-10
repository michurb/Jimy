using Jimy.Application.Abstraction;
using Jimy.Application.DTO;

namespace Jimy.Application.Queries.WorkoutPlans;

public class GetUsersWorkoutPlans : IQuery<IEnumerable<WorkoutPlanDto>>
{
    public Guid UserId { get; set; }
}