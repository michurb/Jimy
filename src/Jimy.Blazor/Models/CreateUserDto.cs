using System.ComponentModel.DataAnnotations;

namespace Jimy.Blazor.Models;

public class CreateUserDto
{
  [Required(ErrorMessage = "Username is required")]
  [StringLength(30, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 30 characters")]
  public string Username { get; set; }

  [Required(ErrorMessage = "Email is required")]
  [EmailAddress(ErrorMessage = "Invalid email address")]
  public string Email { get; set; }

  [Required(ErrorMessage = "Password is required")]
  [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
  public string Password { get; set; }

  [Required(ErrorMessage = "Role is required")]
  public string Role { get; set; }
}