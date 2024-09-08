using Jimy.Business.Abstracition;
using Jimy.Business.Exceptions;
using Jimy.Data.Entities;
using Jimy.Data.Interfaces;

namespace Jimy.Business.Commands.Handler;

public class StartWorkoutSessionHandler : ICommandHandler<StartWorkoutSession>
{
    private readonly IWorkoutSessionRepository _repository;
    private readonly IWorkoutPlanRepository _workoutPlanRepository;

    public StartWorkoutSessionHandler(IWorkoutSessionRepository repository, IWorkoutPlanRepository workoutPlanRepository)
    {
        _repository = repository;
        _workoutPlanRepository = workoutPlanRepository;
    }

    public async Task HandleAsync(StartWorkoutSession command)
    {
        var workoutPlan = await _workoutPlanRepository.GetByIdAsync(command.WorkoutPlanId);
        if (workoutPlan == null)
            throw new WorkoutPlanNotFoundException(command.WorkoutPlanId);

        var workoutSession = new WorkoutSession
        {
            UserId = command.UserId,
            WorkoutPlanId = command.WorkoutPlanId,
            StartTime = DateTime.UtcNow,
            Exercises = command.Exercises.Select(e => new WorkoutSessionExercise
            {
                ExerciseId = e.ExerciseId,
                Sets = e.Sets,
                Reps = e.Reps,
                Weight = e.Weight
            }).ToList()
        };

        await _repository.AddAsync(workoutSession);
        command.SessionId = workoutSession.Id;
    }
}
