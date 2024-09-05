using System.ComponentModel.DataAnnotations;

namespace Jimy.Business.DTOs;

public record UpdateWorkoutPlanDto(
    [Required]
    [StringLength(100)]
    string Name);