namespace Jimy.Business.DTOs;

public record WorkoutExerciseDto(
    int Id,
    int WorkoutPlanId,
    int ExerciseId,
    string ExerciseName,
    int Sets,
    int Reps
);
