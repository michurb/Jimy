using Jimy.Application.Abstraction;

namespace Jimy.Application.Commands;

public record CreateUser(string Username, string Email) : ICommand<Guid>;