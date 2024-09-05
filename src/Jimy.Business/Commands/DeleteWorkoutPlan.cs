using Jimy.Business.Abstracition;

namespace Jimy.Business.Commands;

public record DeleteWorkoutPlan(int Id) : ICommand;