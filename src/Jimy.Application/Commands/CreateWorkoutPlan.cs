using Jimy.Application.Abstraction;
using Jimy.Application.DTO;

namespace Jimy.Application.Commands;

public record CreateWorkoutPlan(Guid UserId, string Name, List<CreateWorkoutExerciseDto> Exercises) : ICommand<int>;