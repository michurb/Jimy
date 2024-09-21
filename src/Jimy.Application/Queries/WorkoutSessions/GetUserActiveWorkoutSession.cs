using System.Collections;
using Jimy.Application.Abstraction;
using Jimy.Application.DTO;

namespace Jimy.Application.Queries.WorkoutPlans;

public class GetUserActiveWorkoutSession : IQuery<WorkoutSessionDto>
{
    public Guid UserId { get; set; }
}