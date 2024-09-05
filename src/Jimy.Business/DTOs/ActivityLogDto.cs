namespace Jimy.Business.DTOs;

public record ActivityLogDto(int Id, int UserId, DateTime Date, string ActivityType, int Duration, int? WorkoutPlanId);