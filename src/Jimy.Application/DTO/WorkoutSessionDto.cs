namespace Jimy.Application.DTO;

public record WorkoutSessionDto(Guid Id, Guid UserId, int WorkoutPlanId, DateTime StartTime, DateTime? EndTime, IEnumerable<WorkoutSessionExerciseDto> Exercises);