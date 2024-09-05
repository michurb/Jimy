namespace Jimy.Business.DTOs;

public record UpdateActivityLogDto(DateTime Date, string ActivityType, int Duration, int? WorkoutPlanId);