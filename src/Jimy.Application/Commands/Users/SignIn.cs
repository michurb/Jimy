using Jimy.Application.Abstraction;

namespace Jimy.Application.Commands.Users;

public record SignIn(string Email, string Password) : ICommand;
