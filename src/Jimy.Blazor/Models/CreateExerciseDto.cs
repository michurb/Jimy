using System.ComponentModel.DataAnnotations;

namespace Jimy.Blazor.Models;

public class CreateExerciseDto
{
  [Required(ErrorMessage = "Name is required")]
  [StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters")]
  public string Name { get; set; }

  [Required(ErrorMessage = "Description is required")]
  [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
  public string Description { get; set; }
}