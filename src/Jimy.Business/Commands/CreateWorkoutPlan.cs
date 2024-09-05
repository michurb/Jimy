using Jimy.Business.Abstracition;
using Jimy.Business.DTOs;

namespace Jimy.Business.Commands;

public record CreateWorkoutPlan(CreateWorkoutPlanDto Dto) : ICommand;