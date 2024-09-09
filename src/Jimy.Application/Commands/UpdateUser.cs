using Jimy.Application.Abstraction;

namespace Jimy.Application.Commands;

public record UpdateUser(Guid Id, string Username, string Email) : ICommand<Guid>;