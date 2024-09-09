using Jimy.Application.Abstraction;

namespace Jimy.Application.Commands;

public record DeleteWorkout(int Id) : ICommand<bool>;