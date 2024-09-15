using System.ComponentModel.DataAnnotations;

namespace Jimy.Blazor.Models;

public class SignUpDto
{
    [Required(ErrorMessage = "Username is required")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 30 characters")]
    public string Username { get; set; }
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is not valid")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Password is required")]
    [StringLength(32, MinimumLength = 6, ErrorMessage = "Password must be at least between 6 characters long")]
    public string Password { get; set; }
    public string Role => "user";
}