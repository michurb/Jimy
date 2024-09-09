using Jimy.Application.Abstraction;
using Jimy.Core.Entities;
using Jimy.Core.Exceptions;
using Jimy.Core.Interfaces;

namespace Jimy.Application.Commands.Handlers;

public class StartWorkoutSessionHandler : ICommandHandler<StartWorkoutSession, bool>
{
    private readonly IWorkoutSessionRepository _workoutSessionRepository;
    private readonly IWorkoutPlanRepository _workoutPlanRepository;

    public StartWorkoutSessionHandler(IWorkoutSessionRepository workoutSessionRepository, IWorkoutPlanRepository workoutPlanRepository)
    {
        _workoutSessionRepository = workoutSessionRepository;
        _workoutPlanRepository = workoutPlanRepository;
    }

    public async Task<bool> Handle(StartWorkoutSession command)
    {
        var workoutPlan = await _workoutPlanRepository.GetByIdAsync(command.WorkoutPlanId);
        if (workoutPlan == null)
        {
            throw new WorkoutPlanNotFoundException(command.WorkoutPlanId);
        }

        var workoutSession = new WorkoutSession(command.UserId, workoutPlan);
        await _workoutSessionRepository.AddAsync(workoutSession);

        return true;
    }
}