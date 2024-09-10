using Jimy.Application.Abstraction;

namespace Jimy.Application.Commands.WorkoutPlans;

public record DeleteWorkoutPlan(Guid Id) : ICommand;