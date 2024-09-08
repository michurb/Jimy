namespace Jimy.Business.DTOs;

public record WorkoutPlanDto(int Id, Guid UserId, string Name, DateTime CreatedDate, List<WorkoutExerciseDto> Exercises);