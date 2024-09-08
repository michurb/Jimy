namespace Jimy.Business.DTOs;

public record UpdateWorkoutSessionExerciseDto(
    int ExerciseId,
    int Sets,
    int Reps,
    decimal Weight
);