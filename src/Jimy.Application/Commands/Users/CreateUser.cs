using Jimy.Application.Abstraction;

namespace Jimy.Application.Commands.Users;

public record CreateUser(Guid UserId, string Username, string Email, string Password) : ICommand;