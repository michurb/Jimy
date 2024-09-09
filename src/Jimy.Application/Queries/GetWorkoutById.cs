using Jimy.Application.Abstraction;
using Jimy.Application.DTO;

namespace Jimy.Application.Queries;

public class GetWorkoutById : IQuery<WorkoutPlanDto>
{
    public int Id { get; set; }
}