namespace Jimy.Application.DTO;

public record WorkoutExerciseDto(Guid Id, Guid ExerciseId, string ExerciseName, int Sets, int Reps);