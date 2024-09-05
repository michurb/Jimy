namespace Jimy.Business.DTOs;

public record UserDto(Guid Id, string Username, string Email, DateTime DateJoined);