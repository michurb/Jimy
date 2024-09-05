using Jimy.Business.Abstracition;
using Jimy.Business.DTOs;

namespace Jimy.Business.Queries;

public record GetWorkoutPlans(Guid UserId) : IQuery<IEnumerable<WorkoutPlanDto>>;