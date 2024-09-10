using Jimy.Application.Abstraction;

namespace Jimy.Application.Commands.Users;

public record DeleteUser(Guid Id) : ICommand;