using Jimy.Business.Abstracition;
using Jimy.Business.DTOs;

namespace Jimy.Business.Queries;

public record GetWorkoutSession(int WorkoutSessionId) : IQuery<WorkoutSessionDto>;