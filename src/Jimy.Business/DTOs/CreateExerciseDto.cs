using System.ComponentModel.DataAnnotations;

namespace Jimy.Business.DTOs;

public record CreateExerciseDto(
    [Required] [StringLength(100)] string Name,
    [StringLength(500)] string Description);