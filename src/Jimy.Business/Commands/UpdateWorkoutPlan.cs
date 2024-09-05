using Jimy.Business.Abstracition;
using Jimy.Business.DTOs;

namespace Jimy.Business.Commands;

public record UpdateWorkoutPlan(int Id, UpdateWorkoutPlanDto Dto) : ICommand;