using Jimy.Application.Abstraction;
using Jimy.Core.Exceptions;
using Jimy.Core.Interfaces;

namespace Jimy.Application.Commands.Handlers;

public class DeleteWorkoutPlanHandler : ICommandHandler<DeleteWorkout, bool>
{
private readonly IWorkoutPlanRepository _workoutRepository;

public DeleteWorkoutPlanHandler(IWorkoutPlanRepository workoutRepository)
{
    _workoutRepository = workoutRepository;
}

public async Task<bool> Handle(DeleteWorkout command)
{
    var workoutPlan = await _workoutRepository.GetByIdAsync(command.Id);
    if (workoutPlan != null)
    {
        await _workoutRepository.DeleteAsync(workoutPlan.Id);
        return true;
    }

    throw new WorkoutPlanNotFoundException(command.Id);
}
}