namespace Jimy.Business.DTOs;

public record CreateActivityLogDto(int UserId, DateTime Date, string ActivityType, int Duration, int? WorkoutPlanId);