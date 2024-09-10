using Jimy.Application.Abstraction;

namespace Jimy.Application.Commands.Users;

public record CreateUser(string Username, string Email, string Password) : ICommand;