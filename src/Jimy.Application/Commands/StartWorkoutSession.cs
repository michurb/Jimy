using Jimy.Application.Abstraction;
using Jimy.Application.DTO;

namespace Jimy.Application.Commands;

public record StartWorkoutSession(Guid UserId, int WorkoutPlanId, List<StartWorkoutSessionExerciseDto> Exercises) : ICommand<int>;