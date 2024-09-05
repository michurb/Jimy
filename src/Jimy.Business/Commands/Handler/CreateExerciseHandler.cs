using AutoMapper;
using Jimy.Business.Abstracition;
using Jimy.Data.Entities;
using Jimy.Data.Interfaces;

namespace Jimy.Business.Commands.Handler;

public class CreateExerciseHandler : ICommandHandler<CreateExercise>
{
    private readonly IMapper _mapper;
    private readonly IExerciseRepository _repository;

    public CreateExerciseHandler(IExerciseRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task HandleAsync(CreateExercise command)
    {
        var exercise = _mapper.Map<Exercise>(command.Dto);
        await _repository.AddAsync(exercise);
    }
}