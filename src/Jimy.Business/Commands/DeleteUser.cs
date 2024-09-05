using Jimy.Business.Abstracition;

namespace Jimy.Business.Commands;

public record DeleteUser(Guid Id) : ICommand;