namespace Jimy.Application.DTO;

public record WorkoutSessionDto(Guid Id, Guid UserId, Guid WorkoutPlanId, DateTime StartTime, DateTime? EndTime, IEnumerable<WorkoutSessionExerciseDto> Exercises);