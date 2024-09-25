namespace Jimy.Application.DTO;

public record ActivityLogDto(Guid Id,
    Guid UserId,
    DateTime Date,
    string ActivityType,
    int Duration,
    Guid? WorkoutPlanId,
    string? WorkoutPlanName);