namespace Jimy.Application.DTO;

public record WorkoutExerciseDto(Guid WorkoutExerciseId, Guid ExerciseId, string ExerciseName, int Sets, int Reps);