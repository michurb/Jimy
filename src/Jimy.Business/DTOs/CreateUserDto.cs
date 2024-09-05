using System.ComponentModel.DataAnnotations;

namespace Jimy.Business.DTOs;

public record CreateUserDto(
    [Required]
    [StringLength(50)]
    string Username,
    
    [Required]
    [EmailAddress]
    [StringLength(100)]
    string Email);