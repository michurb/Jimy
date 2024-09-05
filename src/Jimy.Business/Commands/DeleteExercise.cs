using Jimy.Business.Abstracition;

namespace Jimy.Business.Commands;

public record DeleteExercise(int Id) : ICommand;