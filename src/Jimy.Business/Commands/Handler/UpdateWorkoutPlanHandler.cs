using AutoMapper;
using Jimy.Business.Abstracition;
using Jimy.Business.Exceptions;
using Jimy.Data.Interfaces;

namespace Jimy.Business.Commands.Handler;

public class UpdateWorkoutPlanHandler : ICommandHandler<UpdateWorkoutPlan>
{
    private readonly IMapper _mapper;
    private readonly IWorkoutPlanRepository _repository;

    public UpdateWorkoutPlanHandler(IWorkoutPlanRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task HandleAsync(UpdateWorkoutPlan command)
    {
        var workoutPlan = await _repository.GetByIdAsync(command.Id);
        if (workoutPlan == null)
            throw new WorkoutPlanNotFoundException(command.Id);

        _mapper.Map(command.Dto, workoutPlan);
        await _repository.UpdateAsync(workoutPlan);
    }
}