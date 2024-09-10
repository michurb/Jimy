using Jimy.Application.Abstraction;
using Jimy.Application.DTO;

namespace Jimy.Application.Commands.WorkoutPlans;

public record CreateWorkoutPlan(Guid UserId, string Name, IEnumerable<WorkoutExerciseDto> Exercises) : ICommand;