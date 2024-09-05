using System.ComponentModel.DataAnnotations;

namespace Jimy.Business.DTOs;

public record UpdateUserDto(
    [Required] [StringLength(50)] string Username,
    [Required]
    [EmailAddress]
    [StringLength(100)]
    string Email);