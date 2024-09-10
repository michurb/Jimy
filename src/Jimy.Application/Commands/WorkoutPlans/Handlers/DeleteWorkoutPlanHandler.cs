using Jimy.Application.Abstraction;
using Jimy.Core.Exceptions;
using Jimy.Core.Interfaces;
using Jimy.Core.ValueObjects;

namespace Jimy.Application.Commands.WorkoutPlans.Handlers;

public sealed class DeleteWorkoutPlanHandler(IWorkoutPlanRepository workoutPlanRepository) : ICommandHandler<DeleteWorkoutPlan>
{
    public async Task HandleAsync(DeleteWorkoutPlan command)
    {
        var workoutPlan = await workoutPlanRepository.GetByIdAsync(new WorkoutPlanId(command.Id));
        if (workoutPlan == null)
        {
            throw new WorkoutPlanNotFoundException(command.Id);
        }

        await workoutPlanRepository.DeleteAsync(workoutPlan.Id);
    }
}