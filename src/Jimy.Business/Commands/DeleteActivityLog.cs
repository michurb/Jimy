using Jimy.Business.Abstracition;

namespace Jimy.Business.Commands;

public record DeleteActivityLog(int Id) : ICommand;