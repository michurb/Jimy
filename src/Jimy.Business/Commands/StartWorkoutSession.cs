using Jimy.Business.Abstracition;
using Jimy.Business.DTOs;

namespace Jimy.Business.Commands;

public record StartWorkoutSession(Guid UserId, int WorkoutPlanId, List<WorkoutSessionExerciseInput> Exercises) : ICommand;