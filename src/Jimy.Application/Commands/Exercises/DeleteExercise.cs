using Jimy.Application.Abstraction;

namespace Jimy.Application.Commands.Exercises;

public record DeleteExercise(Guid Id) : ICommand;