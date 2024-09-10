using Jimy.Application.Abstraction;
using Jimy.Application.DTO;

namespace Jimy.Application.Commands.WorkoutSessions;

public record StartWorkoutSession(Guid UserId, Guid WorkoutPlanId, IEnumerable<WorkoutSessionExerciseDto> Exercises) : ICommand;