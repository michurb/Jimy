namespace Jimy.Business.DTOs;

public record WorkoutSessionExerciseDto(
    int Id,
    int ExerciseId,
    string ExerciseName,
    int Sets,
    int Reps,
    decimal Weight
);