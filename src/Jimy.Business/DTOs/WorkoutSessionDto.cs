namespace Jimy.Business.DTOs;

public record WorkoutSessionDto(
    int Id,
    Guid UserId,
    int WorkoutPlanId,
    WorkoutPlanDto WorkoutPlan,
    DateTime StartTime,
    DateTime? EndTime,
    List<WorkoutSessionExerciseDto> Exercises
);