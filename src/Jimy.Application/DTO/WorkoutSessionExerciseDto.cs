namespace Jimy.Application.DTO;

public record WorkoutSessionExerciseDto(Guid Id, Guid ExerciseId, string ExerciseName, int Sets, int Reps, decimal Weight);