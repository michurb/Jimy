using AutoMapper;
using Jimy.Business.Abstracition;
using Jimy.Data.Entities;
using Jimy.Data.Interfaces;

namespace Jimy.Business.Commands.Handler;

public class CreateWorkoutPlanHandler : ICommandHandler<CreateWorkoutPlan>
{
    private readonly IMapper _mapper;
    private readonly IWorkoutPlanRepository _repository;

    public CreateWorkoutPlanHandler(IWorkoutPlanRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task HandleAsync(CreateWorkoutPlan command)
    {
        var workoutPlan = _mapper.Map<WorkoutPlan>(command.Dto);
        await _repository.AddAsync(workoutPlan);
    }
}