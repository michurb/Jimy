using Jimy.Application.Abstraction;

namespace Jimy.Application.Commands.Users;

public record UpdateUser(Guid Id, string Username, string Email, string Role) : ICommand;
