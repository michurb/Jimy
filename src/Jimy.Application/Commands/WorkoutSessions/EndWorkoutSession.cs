using Jimy.Application.Abstraction;
using Jimy.Application.DTO;

namespace Jimy.Application.Commands.WorkoutSessions;

public record EndWorkoutSession(Guid Id, IEnumerable<WorkoutSessionExerciseDto> Exercises) : ICommand;