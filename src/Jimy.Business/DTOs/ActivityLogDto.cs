namespace Jimy.Business.DTOs;

public record ActivityLogDto(int Id, Guid UserId, DateTime Date, string ActivityType, int Duration, int? WorkoutPlanId);