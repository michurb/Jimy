using Jimy.Application.Abstraction;

namespace Jimy.Application.Commands;

public record DeleteUser(Guid Id) : ICommand<Guid>;