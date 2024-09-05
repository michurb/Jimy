using System.ComponentModel.DataAnnotations;

namespace Jimy.Business.DTOs;

public record CreateWorkoutPlanDto(
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "UserId must be greater than 0")]
    Guid UserId,
    
    [Required]
    [StringLength(100)]
    string Name);