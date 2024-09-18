namespace Jimy.Application.DTO;

public record WorkoutSessionExerciseDto(Guid ExerciseId, Guid Id, string Name, int Sets, int Reps, decimal Weight);