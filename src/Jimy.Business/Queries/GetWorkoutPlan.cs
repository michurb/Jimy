using Jimy.Business.Abstracition;
using Jimy.Business.DTOs;

namespace Jimy.Business.Queries;

public record GetWorkoutPlan(int Id) : IQuery<WorkoutPlanDto>;