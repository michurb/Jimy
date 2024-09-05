namespace Jimy.Business.DTOs;

public record WorkoutExerciseDto(int Id, int WorkoutPlanId, int ExerciseId, int Sets, int Reps);