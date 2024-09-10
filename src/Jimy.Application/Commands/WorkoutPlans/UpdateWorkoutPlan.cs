using Jimy.Application.Abstraction;
using Jimy.Application.DTO;

namespace Jimy.Application.Commands.WorkoutPlans;

public record UpdateWorkoutPlan(Guid Id, string Name, IEnumerable<WorkoutExerciseDto> Exercises) : ICommand;