namespace Jimy.Application.DTO;

public record UserDto(Guid Id, string Username, string Email, DateTime DateJoined, string Role)
{
}
