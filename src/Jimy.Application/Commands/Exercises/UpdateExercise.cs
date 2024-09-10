using Jimy.Application.Abstraction;

namespace Jimy.Application.Commands.Exercises;

public record UpdateExercise(Guid Id, string Name, string Description) : ICommand;