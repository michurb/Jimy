using Jimy.Application.Abstraction;

namespace Jimy.Application.Commands.WorkoutSessions;

public record UpdateWorkoutSessionExerciseWeight(Guid SessionId, Guid ExerciseId, int SetNumber, decimal Weight) : ICommand;