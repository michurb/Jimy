namespace Jimy.Application.DTO;

public record WorkoutPlanDto(Guid Id, Guid UserId, string Name, DateTime CreatedDate, IEnumerable<WorkoutExerciseDto> Exercises);