using System.ComponentModel.DataAnnotations;

namespace Jimy.Blazor.Models;

public class EditUserDto
{
  public Guid Id { get; set; }

  [Required(ErrorMessage = "Username is required")]
  [StringLength(30, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 30 characters")]
  public string Username { get; set; }

  [Required(ErrorMessage = "Email is required")]
  [EmailAddress(ErrorMessage = "Invalid email address")]
  public string Email { get; set; }

  [Required(ErrorMessage = "Role is required")]
  public string Role { get; set; }
}