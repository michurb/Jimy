using Jimy.Application.Abstraction;
using Jimy.Application.DTO;

namespace Jimy.Application.Commands;

public record UpdateWorkoutPlan(int Id, string Name, List<UpdateWorkoutExerciseDto> Exercises) : ICommand<int>;