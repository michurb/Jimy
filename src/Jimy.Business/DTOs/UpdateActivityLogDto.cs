using System.ComponentModel.DataAnnotations;

namespace Jimy.Business.DTOs;

public record UpdateActivityLogDto(
    [Required] DateTime Date,
    [Required] [StringLength(50)] string ActivityType,
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Duration must be greater than 0")]
    int Duration,
    [Range(1, int.MaxValue, ErrorMessage = "WorkoutPlanId must be greater than 0")]
    int? WorkoutPlanId);