using Jimy.Application.Abstraction;

namespace Jimy.Application.Commands.Exercises;

public record CreateExercise(string Name, string Description) : ICommand;