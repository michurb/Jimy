using Jimy.Application.Abstraction;
using Jimy.Application.DTO;

namespace Jimy.Application.Queries;

public class GetWorkoutPlansByUserId : IQuery<IEnumerable<WorkoutPlanDto>>
{
    public Guid UserId { get; set; }
}