using System.ComponentModel.DataAnnotations;

namespace Jimy.Business.DTOs;

public record CreateWorkoutPlanDto(
    [Required]
    Guid UserId,
    [Required] [StringLength(100)] string Name,
    [Required]
    List<CreateWorkoutExerciseDto> Exercises);