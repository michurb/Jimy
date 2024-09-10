using Jimy.Application.Abstraction;
using Jimy.Application.DTO;

namespace Jimy.Application.Queries.WorkoutPlans;

public class GetUsersWorkoutSession : IQuery<IEnumerable<WorkoutSessionDto>>
{
    public Guid UserId { get; set; }
}