namespace Jimy.Application.DTO;

public record WorkoutExerciseDto(Guid ExerciseId, string ExerciseName, int Sets, int Reps);