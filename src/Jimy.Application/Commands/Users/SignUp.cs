using Jimy.Application.Abstraction;

namespace Jimy.Application.Commands.Users;

public record SignUp(Guid UserId, string Username, string Email, string Password, string Role) : ICommand;